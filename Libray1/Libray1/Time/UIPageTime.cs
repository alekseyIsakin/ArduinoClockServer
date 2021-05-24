using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

using Lib;
using Lib.HelpingClass;

namespace Lib.Time
{
    class UIPageTime : UIBaseEl
    {
        private readonly TextBox tbX;
        private readonly TextBox tbY;
        private readonly UIAcolorBox clrBox;
        private readonly CheckBox cbSecond;
        private readonly CheckBox cbMinut;
        private readonly CheckBox cbHour;
        private readonly TextBox tbS;

        public UIPageTime(AbstrPageEl pEl)
            : base(47, pEl)
        {
            PageTime pt = (PageTime)pEl;
            SetID(PageTime.ID);

            _customNamePageEl = pt.CustomName;
            _textBoxCustomNamePageEl.Text = _customNamePageEl;

            // Интерфейс для настройки позиции
            Label lbl_pos = new Label();
            tbX = new TextBox();
            tbY = new TextBox();

            lbl_pos.Content = "Позиция";
            lbl_pos.VerticalAlignment = VerticalAlignment.Center;

            tbX.Text = pt.X.ToString();
            tbY.Text = pt.Y.ToString();

            tbX.MaxLength = 3;
            tbY.MaxLength = 3;

            tbX.Width = 25;
            tbY.Width = 25;

            tbX.Height = 23;
            tbY.Height = 23;

            tbX.TextAlignment = TextAlignment.Center;
            tbY.TextAlignment = TextAlignment.Center;

            //

            // Цвет

            clrBox = new UIAcolorBox(
                pt.TextColor.GetColor());
            //

            // Размер
            Label lbl_size = new Label();
            tbS = new TextBox();

            lbl_size.Content = "Размер";
            lbl_size.VerticalAlignment = VerticalAlignment.Center;

            tbS.Text = pt.Size.ToString();

            tbS.Width = 25;
            tbS.Height = 23;
            //

            // Флаги
            StackPanel spFlasgs = new StackPanel();

            cbSecond = new CheckBox();
            cbMinut = new CheckBox();
            cbHour = new CheckBox();

            cbSecond.IsChecked = pt.Second;
            cbMinut.IsChecked = pt.Minute;
            cbHour.IsChecked = pt.Hour;

            cbSecond.Content = "сек";
            cbMinut.Content = "мин";
            cbHour.Content = "час";

            spFlasgs.Children.Add(cbSecond);
            spFlasgs.Children.Add(cbMinut);
            spFlasgs.Children.Add(cbHour);
            //

            //

            _buttonExpand.Content = ID.ToString();
            //
            
            PageContainer.Children.Add(lbl_pos);
            PageContainer.Children.Add(tbX);
            PageContainer.Children.Add(
                UIGenerateHelping.NewGridSplitter(10, PageContainer.Background));
            PageContainer.Children.Add(tbY);

            PageContainer.Children.Add(
                UIGenerateHelping.NewGridSplitter(10, PageContainer.Background));

            PageContainer.Children.Add(clrBox);

            PageContainer.Children.Add(
                UIGenerateHelping.NewGridSplitter(10, PageContainer.Background));

            PageContainer.Children.Add(lbl_size);
            PageContainer.Children.Add(tbS);

            PageContainer.Children.Add(
                UIGenerateHelping.NewGridSplitter(10, PageContainer.Background));

            PageContainer.Children.Add(spFlasgs);
        }
        public override AbstrPageEl CompileElement() 
        {
            if (!byte.TryParse(tbS.Text, out byte sz)) sz = 0;

            if (!byte.TryParse(tbY.Text, out byte py)) py = 0;
            if (!byte.TryParse(tbX.Text, out byte px)) px = 0;

            string customName = _textBoxCustomNamePageEl.Text;
            AColor clr = new AColor(clrBox.GeBoxColor());

            bool sec  = (bool)cbSecond.IsChecked;
            bool min  = (bool)cbMinut.IsChecked;
            bool hour = (bool)cbHour.IsChecked;

            AbstrPageEl p_out = new PageTime(
                px, py, 
                clr, sz);

            ((PageTime)p_out).Second = sec;
            ((PageTime)p_out).Minute = min;
            ((PageTime)p_out).Hour = hour;
            p_out.CustomName = customName;

            return p_out;
        }
    }
}
