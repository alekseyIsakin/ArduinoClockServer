using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;


using ArdClock.src.ArdPage.PageElements;
using ArdClock.src.ArdPage.HelpingClass;

namespace ArdClock.src.ArdPage
{
    class UIAcolorBox : UserControl
    {
        private DockPanel dp;
        private AColor clr;
        private TextBox tbC;
        private Rectangle rectC;

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
            Content = dp;
        }

        public UIAcolorBox(Color clr_in)
            : base()
        {

            dp = new DockPanel();
            tbC = new TextBox();
            rectC = new Rectangle();

            Label lbl_clr = new Label();

            clr = new AColor();
            clr.SetFromColor(clr_in);

            lbl_clr.Content = "Цвет";
            lbl_clr.VerticalAlignment = System.Windows.VerticalAlignment.Center;
            tbC.Text = clr.ToHex();

            tbC.Width = 65;
            tbC.Height = 23;
            tbC.TextAlignment = TextAlignment.Center;
            tbC.MaxLength = 6;

            rectC.Fill = new SolidColorBrush(clr_in);
            rectC.Stroke = Brushes.Black;
            rectC.StrokeThickness = 3;

            rectC.Width = 23;
            rectC.Height = 23;

            rectC.ContextMenu = genContextMenu();

            //rectC.MouseLeftButtonDown +=

            dp.Children.Add(lbl_clr);
            dp.Children.Add(tbC);
            dp.Children.Add(rectC);
            dp.LastChildFill = false;
        }

        public Color GeBoxColor()
        {
            try
            { clr.SetFromHex(tbC.Text); }
            catch
            { return Colors.White; }

            return clr.GetColor(); 
        }

        private ContextMenu genContextMenu() 
        {

            ContextMenu menu = new ContextMenu();

            foreach (string s in AColors.AColorsDict.Keys) 
            {
                MenuItem mi = new MenuItem() { Header = s};
                var r = new Rectangle();
                r.Width = 23;
                r.Height= 23;
                r.Fill = new SolidColorBrush(AColors.AColorsDict[s].GetColor());

                mi.Icon = r;
                menu.Items.Add(mi);
                mi.Click += clickContextMenu;
            }

            
            return menu;
        }

        private void clickContextMenu(object sender, EventArgs e) 
        {
            AColor clr;
            if (sender is MenuItem) 
            {
                string header = (sender as MenuItem).Header as string;

                if (AColors.AColorsDict.ContainsKey(header)) 
                {
                    clr = AColors.AColorsDict[header];
                    tbC.Text = clr.ToHex();
                    rectC.Fill = new SolidColorBrush(clr.GetColor());
                }
            }
        }
    }
}
