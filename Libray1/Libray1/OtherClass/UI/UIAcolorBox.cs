using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Lib.HelpingClass
{
    public class UIAcolorBox : UserControl
    {
        private DockPanel Container;
        private AColor SendColor;
        private TextBox TextBoxColor;
        private Rectangle RectColor;
        ColorBoxSelect ColorSelecter;
        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
            Content = Container;
        }

        public UIAcolorBox(Color clr_in)
            : base()
        {

            Container = new DockPanel();
            TextBoxColor = new TextBox();
            RectColor = new Rectangle();
            ColorSelecter = new ColorBoxSelect();

            Label lbl_clr = new Label();

            SendColor = new AColor();
            SendColor.SetFromColor(clr_in);

            lbl_clr.Content = "Цвет";
            lbl_clr.VerticalAlignment = System.Windows.VerticalAlignment.Center;
            TextBoxColor.Text = SendColor.ToHex();

            TextBoxColor.Width = 65;
            TextBoxColor.Height = 23;
            TextBoxColor.TextAlignment = TextAlignment.Center;
            TextBoxColor.MaxLength = 6;

            RectColor.Fill = new SolidColorBrush(clr_in);
            RectColor.Stroke = Brushes.Black;
            RectColor.StrokeThickness = 3;

            RectColor.Width = 23;
            RectColor.Height = 23;

            RectColor.ContextMenu = genContextMenu();

            ColorSelecter.ColorChange += UpdateColorRect;
            ColorSelecter.SetFromColor(SendColor.GetColor());

            Container.Children.Add(lbl_clr);
            Container.Children.Add(TextBoxColor);
            Container.Children.Add(RectColor);
            Container.Children.Add(ColorSelecter);
            Container.LastChildFill = false;
        }

        public Color GeBoxColor()
        {
            try
            {
                SendColor.SetFromHex(TextBoxColor.Text); 
            }
            catch
            { return Colors.White; }

            return SendColor.GetColor(); 
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
                    TextBoxColor.Text = clr.ToHex();
                    RectColor.Fill = new SolidColorBrush(clr.GetColor());
                    ColorSelecter.SetFromColor(clr.GetColor());
                }
            }
        }
        private void UpdateColorRect(object sender, EventArgs e) 
        {
            if (sender is ColorBoxSelect)
            {
                SendColor.SetFromColor(
                    Color.FromRgb(
                        (sender as ColorBoxSelect).ColorValuePairs['R'],
                        (sender as ColorBoxSelect).ColorValuePairs['G'],
                        (sender as ColorBoxSelect).ColorValuePairs['B']));
                RectColor.Fill = new SolidColorBrush(SendColor.GetColor());
                TextBoxColor.Text = SendColor.ToHex();
            }
        }
    }

    class ColorBoxSelect : UserControl 
    {
        public event EventHandler ColorChange;
        public Dictionary<char, byte> ColorValuePairs;

        Slider SliderR;
        Slider SliderG;
        Slider SliderB;

        protected override void OnInitialized(EventArgs e)
        { base.OnInitialized(e); }

        public ColorBoxSelect() 
        {
            int _height = 10;
            int _width = 100;

            ColorValuePairs = new Dictionary<char, byte>();

            StackPanel sp = new StackPanel();
            SliderR = new Slider();
            SliderG = new Slider();
            SliderB = new Slider();

            SliderR.LargeChange = 8;
            SliderG.LargeChange = 16;
            SliderB.LargeChange = 8;

            SliderR.Uid = "R";
            SliderG.Uid = "G";
            SliderB.Uid = "B";

            ColorValuePairs.Add(SliderR.Uid[0], 0);
            ColorValuePairs.Add(SliderG.Uid[0], 0);
            ColorValuePairs.Add(SliderB.Uid[0], 0);

            SliderR.Maximum = 255;
            SliderG.Maximum = 255;
            SliderB.Maximum = 255;

            SliderR.Height = _height;
            SliderG.Height = _height;
            SliderB.Height = _height;

            SliderR.Width = _width;
            SliderG.Width = _width;
            SliderB.Width = _width;

            SliderR.ValueChanged += Slider_ValueChanged;
            SliderG.ValueChanged += Slider_ValueChanged;
            SliderB.ValueChanged += Slider_ValueChanged;

            sp.Children.Add(SliderR);
            sp.Children.Add(SliderG);
            sp.Children.Add(SliderB);

            Content = sp;
        }

        public void SetFromColor(Color clr) 
        {
            SliderR.Value = clr.R;
            SliderG.Value = clr.G;
            SliderB.Value = clr.B;
        }
        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (sender is Slider)
            {
                ColorValuePairs[(sender as Slider).Uid[0]] = (byte)(sender as Slider).Value;
                if (ColorChange != null)
                    ColorChange(this, null);
            }
        }
    }
}
