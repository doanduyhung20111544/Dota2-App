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
using Dota2_App.Resources;
using System.Windows.Media.Imaging;

namespace Dota2_App.View
{
    public partial class PlaylistPage : PhoneApplicationPage
    {
        private string pre_page="";
        private string next_page="";
        YoutubeMethod ytb = new YoutubeMethod();
        public PlaylistPage()
        {
            InitializeComponent();
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            // TODO: Prepare page for display here.

            //double width = Application.Current.RootVisual.RenderSize.Width - 20;
            


            await ytb.channelInfo();
            gridlist_gridview.ItemsSource = await ytb.playlist_Channel(next_page);
            avatar_image.Source = new BitmapImage(new Uri(ytb.Channel_Avatar, UriKind.RelativeOrAbsolute));
            channelname_textblock.Text = ytb.Channel_Title;
            pre_page = ytb.Pre_Page;
            next_page = ytb.Next_Page;
        }

        private void gridlist_gridview_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            PlaylistInfo myobject = (sender as LongListSelector).SelectedItem as PlaylistInfo;


            if (myobject != null)
            {

                NavigationService.Navigate(new Uri("/View/PlaylistChannelPage.xaml?msg=" + myobject.Id, UriKind.Relative));

            }
        }

        private async void next_button_Click(object sender, RoutedEventArgs e)
        {
            if (next_page != null) 
            {
                gridlist_gridview.ItemsSource = null;
                gridlist_gridview.ItemsSource = await ytb.playlist_Channel(next_page);
                pre_page = ytb.Pre_Page;
                next_page = ytb.Next_Page;
            }
            
        }

        private async void pre_button_Click(object sender, RoutedEventArgs e)
        {
            if (pre_page != null)
            {
                gridlist_gridview.ItemsSource = null;
                gridlist_gridview.ItemsSource = await ytb.playlist_Channel(pre_page);
                pre_page = ytb.Pre_Page;
                next_page = ytb.Next_Page;
            }
        }

    }
}