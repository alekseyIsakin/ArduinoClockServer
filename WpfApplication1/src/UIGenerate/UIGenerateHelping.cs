using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;

namespace ArdClock.src.UIGenerate
{
    static class UIGenerateHelping
    {
        static public GridSplitter NewGridSplitter(int width, Brush bgColor)
        {
            GridSplitter gs = new GridSplitter
            {
                Width = width,
                Background = bgColor
            };

            return gs;
        }
        static public Separator NewSeparator(int height, Brush bgColor)
        {
            Separator gs = new Separator
            {
                Height = height,
                Background = bgColor,
                Margin = new System.Windows.Thickness(0)
            };

            return gs;
        }
    }
}
