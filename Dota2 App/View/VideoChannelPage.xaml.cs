using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Dota2_App.Model;
using System.Windows.Media.Imaging;

namespace Dota2_App.View
{
    public partial class VideoChannelPage : PhoneApplicationPage
    {
        public VideoChannelPage()
        {
            InitializeComponent();
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {

            YoutubeMethod ytb = new YoutubeMethod();
            gridlist_gridview.ItemsSource = await ytb.video_Channel();
            await ytb.channelInfo();
            avatar_image.Source = new BitmapImage(new Uri(ytb.Channel_Avatar, UriKind.RelativeOrAbsolute));
            
        }

        private void HardwareButtons_BackPressed(object sender, Windows.Phone.UI.Input.BackPressedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void gridlist_gridview_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            VideoInfo myobject = (sender as LongListSelector).SelectedItem as VideoInfo;


            if (myobject != null)
            {

                NavigationService.Navigate(new Uri("/View/VideoPage.xaml?msg=" + myobject.Id, UriKind.Relative));

            }
        }
    }
}