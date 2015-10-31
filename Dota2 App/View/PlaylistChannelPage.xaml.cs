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
    public partial class PlaylistChannelPage : PhoneApplicationPage
    {
        
        public PlaylistChannelPage()
        {
            InitializeComponent();
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            //ScreenResolution screen = new ScreenResolution();
            
            

            base.OnNavigatedTo(e);

            string msg = "";
            string playlist_id="";
            if (NavigationContext.QueryString.TryGetValue("msg", out msg)) playlist_id = msg;
            YoutubeMethod ytb = new YoutubeMethod();
            gridlist_gridview.ItemsSource = await ytb.get_video_Playlist(playlist_id);
            await ytb.channelInfo();
            avatar_image.Source = new BitmapImage(new Uri(ytb.Channel_Avatar, UriKind.RelativeOrAbsolute));
            channelname_textblock.Text = ytb.Channel_Title;
            //gridlist_gridview.Visibility = Visibility.Visible;
            
            
        }

        private void HardwareButtons_BackPressed(object sender, Windows.Phone.UI.Input.BackPressedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void gridlist_gridview_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            VideoInfo myobject = (sender as LongListSelector).SelectedItem as VideoInfo;
            System.Diagnostics.Debug.WriteLine(myobject.Id);

            if (myobject != null)
            {

                NavigationService.Navigate(new Uri("/View/VideoPage.xaml?msg=" + myobject.Id, UriKind.Relative));

            }
        }
    }
}