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
    /// Interaction logic for Article.xaml
    /// </summary>
    public partial class Article : Page
    {
        public Article()
        {
            InitializeComponent();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            new SoundPlayer(Environment.CurrentDirectory + @"\Content\Audio\Button Select.wav").Play();
            if (NavigationService != null) NavigationService.Navigate(new Uri("MenuPage.xaml", UriKind.Relative));
        }

        private void ExitBtn_OnMouseEnter(object sender, MouseEventArgs e)
        {
            new SoundPlayer(Environment.CurrentDirectory + @"\Content\Audio\Button_Hover.wav").Play();
        }

        private void ExitBtn_OnHandPointerEnter(object sender, HandPointerEventArgs e)
        {
            new SoundPlayer(Environment.CurrentDirectory + @"\Content\Audio\Button_Hover.wav").Play();
        }
    }
}
