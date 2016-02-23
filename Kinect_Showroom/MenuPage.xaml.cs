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
using Microsoft.Kinect.Toolkit.Controls;

namespace Kinect_Showroom
{
    /// <summary>
    /// Interaction logic for MenuPage.xaml
    /// </summary>
    public partial class MenuPage : Page
    {
        public MenuPage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// When clicked play a sound and load the appropriate page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ArticleButton_OnClick(object sender, RoutedEventArgs e)
        {
            new SoundPlayer(Environment.CurrentDirectory + @"\Content\Audio\Button Select.wav").Play();
            if (NavigationService != null) NavigationService.Navigate(new Uri("Article.xaml",UriKind.Relative));
        }

        /// <summary>
        /// When clicked play a sound and load the appropriate page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MapButton_OnClick(object sender, RoutedEventArgs e)
        {
            new SoundPlayer(Environment.CurrentDirectory + @"\Content\Audio\Button Select.wav").Play();
            if (NavigationService != null) NavigationService.Navigate(new Uri("KinectMap.xaml", UriKind.Relative));
        }

        /// <summary>
        /// When clicked play a sound and load the appropriate page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PhotosButton_OnClick(object sender, RoutedEventArgs e)
        {
            new SoundPlayer(Environment.CurrentDirectory + @"\Content\Audio\Button Select.wav").Play();
            if (NavigationService != null) NavigationService.Navigate(new Uri("Photos.xaml", UriKind.Relative));
        }

        /// <summary>
        /// When clicked play a sound and exit the application
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExitButton_OnClick(object sender, RoutedEventArgs e)
        {
            new SoundPlayer(Environment.CurrentDirectory + @"\Content\Audio\Button Select.wav").Play();
            Application.Current.Shutdown();
        }

        private void ArticleButton_OnMouseEnter(object sender, MouseEventArgs e)
        {
            new SoundPlayer(Environment.CurrentDirectory + @"\Content\Audio\Button_Hover.wav").Play();
        }

        private void ArticleButton_OnHandPointerEnter(object sender, HandPointerEventArgs e)
        {
            new SoundPlayer(Environment.CurrentDirectory + @"\Content\Audio\Button_Hover.wav").Play();
        }

        private void MapButton_OnMouseEnter(object sender, MouseEventArgs e)
        {
            new SoundPlayer(Environment.CurrentDirectory + @"\Content\Audio\Button_Hover.wav").Play();
        }

        private void MapButton_OnHandPointerEnter(object sender, HandPointerEventArgs e)
        {
            new SoundPlayer(Environment.CurrentDirectory + @"\Content\Audio\Button_Hover.wav").Play();
        }

        private void PhotosButton_OnMouseEnter(object sender, MouseEventArgs e)
        {
            new SoundPlayer(Environment.CurrentDirectory + @"\Content\Audio\Button_Hover.wav").Play();
        }

        private void PhotosButton_OnHandPointerEnter(object sender, HandPointerEventArgs e)
        {
            new SoundPlayer(Environment.CurrentDirectory + @"\Content\Audio\Button_Hover.wav").Play();
        }

        private void ExitButton_OnMouseEnter(object sender, MouseEventArgs e)
        {
            new SoundPlayer(Environment.CurrentDirectory + @"\Content\Audio\Button_Hover.wav").Play();
        }

        private void ExitButton_OnHandPointerEnter(object sender, HandPointerEventArgs e)
        {
            new SoundPlayer(Environment.CurrentDirectory + @"\Content\Audio\Button_Hover.wav").Play();
        }
    }
}
