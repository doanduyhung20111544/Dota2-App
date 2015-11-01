using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Dota2_App.Resources;
using Dota2_App.Model;
using System.Windows.Media.Imaging;

namespace Dota2_App
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();

            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            // TODO: Prepare page for display here.

            //double width = Application.Current.RootVisual.RenderSize.Width - 20;
            YoutubeMethod ytb = new YoutubeMethod();


            await ytb.channelInfo();
            gridlist_gridview.ItemsSource = await ytb.video_Channel();
            avatar_image.Source = new BitmapImage(new Uri(ytb.Channel_Avatar, UriKind.RelativeOrAbsolute));
            channelname_textblock.Text = ytb.Channel_Title;

        }

        private void Border_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/View/PlaylistPage.xaml", UriKind.Relative));
        }

        private void gridlist_gridview_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            VideoInfo myobject = (sender as LongListSelector).SelectedItem as VideoInfo;


            if (myobject != null)
            {

                NavigationService.Navigate(new Uri("/View/VideoPage.xaml?msg=" + myobject.Id, UriKind.Relative));

            }
        }

        private void Image_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            
            NavigationService.Navigate(new Uri("/View/ChangeChannel.xaml", UriKind.Relative));
        }
    }
}