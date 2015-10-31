using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dota2_App.Model
{
    class PlaylistInfo
    {
        public PlaylistInfo() { }
        public PlaylistInfo(string id, string title, string date, Uri thumnai, long? number)
        {
            Id = id;
            Title = title;
            Date = date;
            Thumnai = thumnai;
            Number = number + " Video";
        }

        public string Id { get; set; }

        public string Title { get; set; }

        public string Date { get; set; }

        public Uri Thumnai { get; set; }

        public string Number { get; set; }
    }
}
