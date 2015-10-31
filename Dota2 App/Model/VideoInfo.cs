using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dota2_App.Model
{
    class VideoInfo
    {
        public VideoInfo() { }
        public VideoInfo(string id, string title, string date, Uri thumnai, string view)
        {
            Id = id;
            Title = title;
            Date = date;
            Thumnai = thumnai;
            View = view;
        }

        public string Id { get; set; }

        public string Title { get; set; }

        public string Date { get; set; }

        public Uri Thumnai { get; set; }

        public string View { get; set; }
    }
}
