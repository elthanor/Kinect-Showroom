using System;
using System.Collections.Generic;
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
using Microsoft.Kinect.Toolkit;
using Microsoft.Kinect.Toolkit.Controls;

namespace Kinect_Showroom
{
    /// <summary>
    /// Interaction logic for KinectMap.xaml
    /// </summary>
    public partial class KinectMap : Page
    {
        private KinectRegion _kinectRegion;
        private HandEventType[] _lastHandEvents = new HandEventType[2];
        private double[] _previousX = new double[2];
        public KinectMap()
        {
            InitializeComponent();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            new SoundPlayer(Environment.CurrentDirectory + @"\Content\Audio\Button Select.wav").Play();
            if (NavigationService != null) NavigationService.Navigate(new Uri("MenuPage.xaml",UriKind.Relative));
        }

        /// <summary>
        /// When the page is loaded get the kinect sensor from the application properties and subscribe to the handpointers_updated event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void KinectMap_OnLoaded(object sender, RoutedEventArgs e)
        {
            if ((KinectRegion)Application.Current.Properties["KinectRegionProp"]!=null)
                _kinectRegion=(KinectRegion)Application.Current.Properties["KinectRegionProp"];
            _kinectRegion.HandPointersUpdated += _kinectRegion_HandPointersUpdated;
        }

        /// <summary>
        /// If the two hand are in grip mode track the horizontal distance between them and zoom the map accordingly
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void _kinectRegion_HandPointersUpdated(object sender, EventArgs e)
        {
            foreach (
                HandPointer hand in
                    _kinectRegion.HandPointers.Where(
                        x => x.HandEventType != HandEventType.None && x.IsPrimaryUser.Equals(true)))
                _lastHandEvents[hand.HandType == HandType.Left ? 0 : 1] = hand.HandEventType;
            if (_kinectRegion.HandPointers.Count(x => x.IsInteractive && x.IsPrimaryUser) > 1 &&
                _lastHandEvents.Count(x => x.Equals(HandEventType.Grip)) > 1)
            {
                HandPointer leftHand =
                    _kinectRegion.HandPointers.Single(
                        x => x.IsInteractive && x.HandType.Equals(HandType.Left) && x.IsPrimaryUser);
                HandPointer rightHand =
                    _kinectRegion.HandPointers.Single(
                        x => x.IsInteractive && x.HandType.Equals(HandType.Right) && x.IsPrimaryUser);
                if (_previousX[0].Equals(0) && _previousX[1].Equals(0))
                {
                    _previousX[0] = leftHand.GetPosition(_kinectRegion).X;
                    _previousX[1] = rightHand.GetPosition(_kinectRegion).X;
                }
                MapImage.Width *= (_previousX[0]/leftHand.GetPosition(_kinectRegion).X +
                                   rightHand.GetPosition(_kinectRegion).X/_previousX[1])/2;
                if (MapImage.Width > MapImage.MaxWidth) MapImage.Width = MapImage.MaxWidth;
                _previousX[0] = leftHand.GetPosition(_kinectRegion).X;
                _previousX[1] = rightHand.GetPosition(_kinectRegion).X;
            }
            else _previousX[0] = _previousX[1] = 0;
        }

        private void BackBtn_OnMouseEnter(object sender, MouseEventArgs e)
        {
            new SoundPlayer(Environment.CurrentDirectory + @"\Content\Audio\Button_Hover.wav").Play();
        }

        private void BackBtn_OnHandPointer(object sender, HandPointerEventArgs e)
        {
            new SoundPlayer(Environment.CurrentDirectory + @"\Content\Audio\Button_Hover.wav").Play();
        }
    }
}
