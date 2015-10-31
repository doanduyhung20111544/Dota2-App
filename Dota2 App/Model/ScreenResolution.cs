using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;

namespace Dota2_App.Model
{
    class ScreenResolution
    {
        double width = Application.Current.Host.Content.ActualWidth;
        double height = Application.Current.Host.Content.ActualHeight;

        public Size ScreenSize
        {
            get { return new Size((int)(width - 20) / 2, (int)height / 3.7); }
        }

        public Size MaxSize
        {
            get { return new Size((int)width, (int)height / 3); }
        }
    }
}
