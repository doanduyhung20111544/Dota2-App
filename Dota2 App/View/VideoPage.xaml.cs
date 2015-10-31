using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using MyToolkit.Multimedia;
using System.Windows.Threading;
using System.Windows.Media;
using System.Windows.Media.Imaging;


namespace Dota2_App.View
{
    public partial class VideoPage : PhoneApplicationPage
    {

        private DispatcherTimer timer;
        private bool _sliderpressed = false;

        public VideoPage()
        {
            
            InitializeComponent();
            
            
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            
            SystemTray.IsVisible = false;
            base.OnNavigatedTo(e);
            string msg = "";
            string id = "";
            if (NavigationContext.QueryString.TryGetValue("msg", out msg)) id = msg;
            System.Diagnostics.Debug.WriteLine("----------------" + id);
            var url = await YouTube.GetVideoUriAsync(id, YouTubeQuality.Quality480P);
            video_element.Source = url.Uri;
           
            
            
            
        }

        private void HardwareButtons_BackPressed(object sender, Windows.Phone.UI.Input.BackPressedEventArgs e)
        {
            NavigationService.GoBack();
            SystemTray.IsVisible = true;
        }

        private void video_element_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            if (!_sliderpressed)
            {
                if (video_element.CurrentState == MediaElementState.Playing)
                {
                    video_element.Pause();
                    timer.Stop();
                    center_image.Source = new BitmapImage(new Uri("/View/PauseIcon.png", UriKind.RelativeOrAbsolute));
                    image_sb.Begin();
                    slider_sb.Begin();

                }
                if (video_element.CurrentState == MediaElementState.Paused)
                {
                    video_element.Play();
                    timer.Start();
                    center_image.Source = new BitmapImage(new Uri("/View/PlayIcon.png", UriKind.RelativeOrAbsolute));
                    image_sb.Begin();
                    slider_sb.Begin();
                }
            }
           
        }

        private void video_element_MediaOpened(object sender, RoutedEventArgs e)
        {
            double absvalue = (int)Math.Round(
            video_element.NaturalDuration.TimeSpan.TotalSeconds,
            MidpointRounding.AwayFromZero);
            timelineSlider.Maximum = absvalue;
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(250);
            timer_Start();
            
        }

        private void timer_Start()
        {
            timer.Tick += timer_Tick;
            timer.Start();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if (!_sliderpressed)
            {
                timelineSlider.Value = video_element.Position.TotalSeconds;
            }
            
        }

        private void timelineSlider_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            _sliderpressed = true;
        }

        private void timelineSlider_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            video_element.Position = TimeSpan.FromSeconds(timelineSlider.Value);
            _sliderpressed = false;
        }

        void timelineSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (_sliderpressed)
            {
                video_element.Position = TimeSpan.FromSeconds(e.NewValue);
            }
        }

        private void video_element_MediaEnded(object sender, RoutedEventArgs e)
        {
            
            center_image.Source = new BitmapImage(new Uri("/View/PlayIcon.png", UriKind.RelativeOrAbsolute));
            center_image.Opacity = 1;
            timelineSlider.Value = 0.0;
            timer.Stop();
        }

        private void center_image_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            String state = video_element.CurrentState.ToString();
            System.Diagnostics.Debug.WriteLine(state);
                switch (state)
                {
                    case "Playing":
                        {
                            video_element.Pause();
                            timer.Stop();
                            center_image.Source = new BitmapImage(new Uri("/View/PauseIcon.png", UriKind.RelativeOrAbsolute));
                            image_sb.Begin();
                            slider_sb.Begin();
                        }
                        break;
                    case "Paused":
                        {
                            video_element.Play();
                            timer.Start();
                            center_image.Source = new BitmapImage(new Uri("/View/PlayIcon.png", UriKind.RelativeOrAbsolute));
                            image_sb.Begin();
                            slider_sb.Begin();
                            //ImageBrush imageBrush = new ImageBrush();
                            //Uri uri = new Uri("/View/PauseIcon.png", UriKind.RelativeOrAbsolute);
                            //imageBrush.ImageSource = new BitmapImage(uri);
                        }
                        break;
                    case "Stopped":
                        {
                            video_element.Play();
                            timer.Start();
                            center_image.Source = new BitmapImage(new Uri("/View/PlayIcon.png", UriKind.RelativeOrAbsolute));
                            image_sb.Begin();
                            slider_sb.Begin();
                        }
                        break;
                    default: break;
                }
            
            
        }

        private void timelineSlider_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
                slider_sb.Begin();
        }



        

    }
}