using System;
using System.Collections.Generic;
using System.IO;
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

namespace Kinect_Showroom
{
    /// <summary>
    /// Interaction logic for Photos.xaml
    /// </summary>
    public partial class Photos : Page
    {
        private KinectRegion _kinectRegion;
        private HandEventType[] _lastHandEvents=new HandEventType[2];
        private int _rotateAngle;
        private double[] _previousY=new double[2];
        private Dictionary<long, double> _handFrames = new Dictionary<long, double>();
        private string[] _imagePaths = new string[3]
        {
            "pack://application:,,,/Content/Images/NewZealand.jpg",
            "pack://application:,,,/Content/Images/Leap Of Faith.jpg",
            "pack://application:,,,/Content/Images/marspic_1024.jpg"
        };
        private long _lastGestureTimestamp;
        public Photos()
        {
            InitializeComponent();
        }

        /// <summary>
        /// When window is loaded check get the kinect sensor property and subscribe to HandsPointerUpdated event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Photos_OnLoaded(object sender, RoutedEventArgs e)
        {
            if ((KinectRegion)Application.Current.Properties["KinectRegionProp"] != null) 
                _kinectRegion = (KinectRegion)Application.Current.Properties["KinectRegionProp"];
            _kinectRegion.HandPointersUpdated += _kinectRegion_HandPointersUpdated;
            ImageViewbox.Height =ActualHeight - 100;
        }

        /// <summary>
        /// Track the primary hand on screen and check if it performs a left/right swipe.If it does change the picture accordingly.
        /// Also check if the two hands are in grip mode and if they are rotate the image according to the difference of their height.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void _kinectRegion_HandPointersUpdated(object sender, EventArgs e)
        {
            foreach (
                HandPointer hand in
                    _kinectRegion.HandPointers.Where(x => x.HandEventType != HandEventType.None && x.IsPrimaryUser))
                _lastHandEvents[hand.HandType == HandType.Left ? 0 : 1] = hand.HandEventType;
            if (_kinectRegion.HandPointers.Count(x => x.IsInteractive && x.IsPrimaryUser) > 1 &&
                _lastHandEvents.Count(x => x.Equals(HandEventType.Grip)) > 1)
            {
                HandPointer leftHand =
                    _kinectRegion.HandPointers.First(
                        x => x.IsInteractive && x.HandType.Equals(HandType.Left) && x.IsPrimaryUser);
                HandPointer rightHand =
                    _kinectRegion.HandPointers.First(
                        x => x.IsInteractive && x.HandType.Equals(HandType.Right) && x.IsPrimaryUser);
                if (_previousY[0].Equals(0) && _previousY[1].Equals(0))
                {
                    _previousY[0] = leftHand.GetPosition(_kinectRegion).Y;
                    _previousY[1]= rightHand.GetPosition(_kinectRegion).Y;
                }
                _rotateAngle +=
                    Convert.ToInt32(((_previousY[0] - leftHand.GetPosition(_kinectRegion).Y +
                                      rightHand.GetPosition(_kinectRegion).Y - _previousY[1])*180)/
                                    (2*_kinectRegion.ActualHeight));
                PhotoImage.LayoutTransform=new RotateTransform(_rotateAngle);
                _previousY[0] = leftHand.GetPosition(_kinectRegion).Y;
                _previousY[1] = rightHand.GetPosition(_kinectRegion).Y;
                return;
            }
            _previousY[0] = _previousY[1] = 0;

            foreach (
                HandPointer hand in
                    _kinectRegion.HandPointers.Where(
                        x =>
                            x.IsPrimaryUser && x.IsPrimaryHandOfUser &&
                            x.TimestampOfLastUpdate - _lastGestureTimestamp > 600 &&
                            _lastHandEvents[x.HandType == HandType.Left ? 0 : 1] != HandEventType.Grip))
            {   
                _handFrames.Add(hand.TimestampOfLastUpdate,hand.GetPosition(_kinectRegion).X);
                foreach (var val in _handFrames.ToList())
                    if (hand.TimestampOfLastUpdate - val.Key > 500) _handFrames.Remove(val.Key);
                foreach (var val in _handFrames.ToList())
                {
                    if (hand.GetPosition(_kinectRegion).X - val.Value > 1000)
                    {
                        for (int i = 0; i < _imagePaths.Length; i++)
                        {
                            if (PhotoImage.Source == null || !PhotoImage.Source.ToString().Contains(_imagePaths[i]) ||
                                i <= 0) continue;
                            PhotoImage.Source =
                                (ImageSource) new ImageSourceConverter().ConvertFrom(new Uri(@_imagePaths[i - 1]));
                            break;
                        }
                        _lastGestureTimestamp = hand.TimestampOfLastUpdate;
                        break;
                    }
                    if (val.Value - hand.GetPosition(_kinectRegion).X > 1000)
                    {
                        for (int i = 0; i < _imagePaths.Length; i++)
                        {
                            if (PhotoImage.Source == null || !PhotoImage.Source.ToString().Contains(_imagePaths[i]) ||
                                i >= _imagePaths.Length - 1) continue;
                            PhotoImage.Source =
                                (ImageSource) new ImageSourceConverter().ConvertFrom(new Uri(@_imagePaths[i + 1]));
                            break;
                        }
                        _lastGestureTimestamp = hand.TimestampOfLastUpdate;
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// When clicked either from a mouse or a hand pointer play a sound and load the new page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            new SoundPlayer(Environment.CurrentDirectory + @"\Content\Audio\Button Select.wav").Play();
            if (NavigationService != null) NavigationService.Navigate(new Uri("MenuPage.xaml", UriKind.Relative));
        }

        /// <summary>
        /// Play sound when mouse pointer enters
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CircleBtn_OnMouseEnter(object sender, MouseEventArgs e)
        {
            new SoundPlayer(Environment.CurrentDirectory + @"\Content\Audio\Button_Hover.wav").Play();
        }

        /// <summary>
        /// Play sound when hand pointer enters
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CircleBtn_OnHandPointerEnter(object sender, HandPointerEventArgs e)
        {
            new SoundPlayer(Environment.CurrentDirectory + @"\Content\Audio\Button_Hover.wav").Play();
        }
    }
}
