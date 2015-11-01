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
    public partial class ChangeChannel : PhoneApplicationPage
    {
        YoutubeMethod ytb = new YoutubeMethod();
        public ChangeChannel()
        {
            InitializeComponent();
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            // TODO: Prepare page for display here.

            //double width = Application.Current.RootVisual.RenderSize.Width - 20;
            


            await ytb.channelInfo();
            avatar_image.Source = new BitmapImage(new Uri(ytb.Channel_Avatar, UriKind.RelativeOrAbsolute));
            channelname_textblock.Text = ytb.Channel_Title;

        }

        private async void pre_button_Click(object sender, RoutedEventArgs e)
        {
            //System.IO.StreamWriter file = new System.IO.StreamWriter("ChannelList.txt", true);

            //file.Write(s);
            
            await ytb.change_channel_id(new_channel.Text);
            //NavigationService.GoBack();
            
            NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
        }

        private void next_button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}