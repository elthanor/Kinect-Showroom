using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Kinect;
using Microsoft.Kinect.Toolkit;
using Microsoft.Kinect.Toolkit.Controls;
using System.IO;

namespace Kinect_Showroom
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private KinectSensorChooser _sensorChooser;

        public MainWindow()
        {
            new SplashScreen("Content/Images/NewZealand.jpg").Show(true);
            InitializeComponent();
        }

        /// <summary>
        /// Initialize the new kinect sensor and dispose the old one
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SensorChooserOnKinectChanged(object sender, KinectChangedEventArgs e)
        {
            if (e.OldSensor != null)
            {
                try
                {
                    e.OldSensor.DepthStream.Range = DepthRange.Default;
                    e.OldSensor.SkeletonStream.EnableTrackingInNearRange = false;
                    e.OldSensor.DepthStream.Disable();
                    e.OldSensor.SkeletonStream.Disable();
                }
                catch (InvalidOperationException) { }
            }
            if (e.NewSensor == null) return;
            try
            {
                e.NewSensor.DepthStream.Enable(DepthImageFormat.Resolution640x480Fps30);
                e.NewSensor.SkeletonStream.Enable();
                try
                {
                    e.NewSensor.DepthStream.Range = DepthRange.Near;
                    e.NewSensor.SkeletonStream.EnableTrackingInNearRange = true;
                }
                catch (InvalidOperationException)
                {
                    e.NewSensor.DepthStream.Range = DepthRange.Default;
                    e.NewSensor.SkeletonStream.EnableTrackingInNearRange = false;
                }
            }
            catch (InvalidOperationException) { }
        }

        /// <summary>
        /// When the window is closed stop the sensor
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainWindow_OnClosing(object sender, CancelEventArgs e)
        {
            _sensorChooser.Stop();
        }

        /// <summary>
        /// When the window appears initialize a kinect sensor if connected, set it to an application property and subscribe to the events
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            _sensorChooser = new KinectSensorChooser();
            _sensorChooser.KinectChanged += SensorChooserOnKinectChanged;
            SensorChooserUi.KinectSensorChooser = _sensorChooser;
            _sensorChooser.Start();

            BindingOperations.SetBinding(KinectRegionProp, KinectRegion.KinectSensorProperty,
                new Binding("Kinect") {Source = _sensorChooser});

            Application.Current.Properties["KinectRegionProp"] = KinectRegionProp;
            NavFrame.NavigationService.Navigate(new Uri("MenuPage.xaml", UriKind.Relative));
        }
    }
}
