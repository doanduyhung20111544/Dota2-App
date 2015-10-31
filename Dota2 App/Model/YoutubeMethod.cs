using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Apis.Services;
using Google.Apis.YouTube.v3;


namespace Dota2_App.Model
{
    class YoutubeMethod
    {
        YouTubeService youtube = new YouTubeService(new BaseClientService.Initializer()
        {
            ApplicationName = "Dota2",
            ApiKey = "AIzaSyAjUO7brgZTm3iaIxwbsIM1TOUF_kpen0U",
        });

        private string channel_avatar;
        private string channel_id;
        private string channel_title;
        private string s = "UCDR8cvjALazMm2j9hOar8_g";
        private List<VideoInfo> videochannellist = new List<VideoInfo>();
        private List<PlaylistInfo> playlistchannel = new List<PlaylistInfo>();
        private List<VideoInfo> videoplaylist = new List<VideoInfo>();

        internal async Task<List<VideoInfo>> video_Channel()
        {
            var channelVideoRequest = youtube.Search.List("snippet");
            channelVideoRequest.ChannelId = s;
            channelVideoRequest.Type = "video";
            channelVideoRequest.MaxResults = 20;
            channelVideoRequest.Order = Google.Apis.YouTube.v3.SearchResource.ListRequest.OrderEnum.Date;
            var channelVideoResponse = await channelVideoRequest.ExecuteAsync();
            foreach (var video in channelVideoResponse.Items)
            {

                var videoRequest = youtube.Videos.List("statistics");
                videoRequest.Id = video.Id.VideoId;
                var videoResponse = await videoRequest.ExecuteAsync();

                videochannellist.Add(
                    new VideoInfo
                        (
                            video.Id.VideoId.ToString(),
                            video.Snippet.Title.ToString(),
                            String.Format("{0:MM/dd/yyyy}", video.Snippet.PublishedAt),
                            new Uri(video.Snippet.Thumbnails.Default__.Url.ToString(), UriKind.RelativeOrAbsolute),
                            videoResponse.Items[0].Statistics.ViewCount.ToString()
                        )
                    );
            }
            return videochannellist;
        }

        internal async Task<List<PlaylistInfo>> playlist_Channel()
        {

            var nextPageToken = "";
            while (nextPageToken != null)
            {
                var playlistChannelRequest = youtube.Playlists.List("snippet");
                playlistChannelRequest.ChannelId = s;
                playlistChannelRequest.PageToken = nextPageToken;
                var playlistChannelResponse = await playlistChannelRequest.ExecuteAsync();

                foreach (var playlist in playlistChannelResponse.Items)
                {
                    var videoPlaylistRequest = youtube.PlaylistItems.List("contentDetails");
                    videoPlaylistRequest.PlaylistId = playlist.Id.ToString();
                    var videoPlaylistResponse = await videoPlaylistRequest.ExecuteAsync();
                    playlistchannel.Add(
                        new PlaylistInfo
                            (
                                playlist.Id.ToString(),
                                playlist.Snippet.Title.ToString(),
                                String.Format("{0:MM/dd/yyyy}", playlist.Snippet.PublishedAt),
                                new Uri(playlist.Snippet.Thumbnails.High.Url.ToString(), UriKind.RelativeOrAbsolute),
                                videoPlaylistResponse.PageInfo.TotalResults

                            )
                        );
                }
                nextPageToken = playlistChannelResponse.NextPageToken;
            }

            return playlistchannel;
        }

        internal async Task<List<VideoInfo>> get_video_Playlist(string playlist)
        {
            var nextPageToken = "";
            while (nextPageToken != null)
            {
                var videoPlaylistRequest = youtube.PlaylistItems.List("snippet");
                videoPlaylistRequest.PlaylistId = playlist;
                videoPlaylistRequest.PageToken = nextPageToken;
                var videoPlaylistResponse = await videoPlaylistRequest.ExecuteAsync();
                foreach (var video in videoPlaylistResponse.Items)
                {

                    var videoRequest = youtube.Videos.List("statistics");
                    videoRequest.Id = video.Snippet.ResourceId.VideoId;
                    var videoResponse = await videoRequest.ExecuteAsync();

                    videoplaylist.Add(
                        new VideoInfo
                            (
                                video.Snippet.ResourceId.VideoId.ToString(),
                                video.Snippet.Title.ToString(),
                                String.Format("{0:MM/dd/yyyy}", video.Snippet.PublishedAt),
                                new Uri(video.Snippet.Thumbnails.Default__.Url.ToString(), UriKind.RelativeOrAbsolute),
                                videoResponse.Items[0].Statistics.ViewCount.ToString()
                            )
                        );
                }
                nextPageToken = videoPlaylistResponse.NextPageToken;
            }

            return videoplaylist;
        }

        internal async Task channelInfo()
        {
            var channelInfoRequest = youtube.Channels.List("snippet");
            channelInfoRequest.Id = s;
            var channelInfoResponse = await channelInfoRequest.ExecuteAsync();
            channel_id = channelInfoResponse.Items[0].Id;
            channel_avatar = channelInfoResponse.Items[0].Snippet.Thumbnails.Default__.Url.ToString();
            channel_title = channelInfoResponse.Items[0].Snippet.Title.ToString();
        }

        public string Channel_id
        {
            get { return channel_id; }
        }

        public string Channel_Avatar
        {
            get { return channel_avatar; }
        }

        public string Channel_Title
        {
            get { return channel_title; }
        }
    }
}
