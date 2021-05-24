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

namespace Lib.String
{
    public class UIPageString : UIBaseEl
    {
        private readonly TextBox tbX;
        private readonly TextBox tbY;
        private readonly UIAcolorBox clrBox;
        private readonly TextBox tbS;
        private readonly TextBox tbT;

        public UIPageString(AbstrPageEl pEl)
            : base(60, pEl)
        {
            PageString ps = (PageString)pEl;
            SetID(PageString.ID);

            _customNamePageEl = ps.CustomName;
            _textBoxCustomNamePageEl.Text = _customNamePageEl;

            // Интерфейс для настройки позиции
            Label lbl_pos = new Label();
            tbX = new TextBox();
            tbY = new TextBox();

            lbl_pos.Content = "Позиция";
            tbX.Text = ps.X.ToString();
            tbY.Text = ps.Y.ToString();

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
                ps.TextColor.GetColor());

            // Размер
            Label lbl_size = new Label();
            tbS = new TextBox();

            lbl_size.Content = "Размер";
            tbS.Text = ps.Size.ToString();

            tbS.Width = 25;
            tbS.Height = 23;
            //

            // Текст
            tbT = new TextBox();

            tbT.Height = 23;
            tbT.Text = ps.Data;

            DockPanel.SetDock(tbT, Dock.Bottom);
            //

            //
            _buttonExpand.Content = ID.ToString();
            //

            PageContainer.Children.Add(tbT);

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

            clrBox.Uid = "clrBox";
            tbX.Uid = "tbX";
            tbY.Uid = "tbY";
            tbS.Uid = "tbS";
            tbT.Uid = "tbT";
        }

        public override AbstrPageEl CompileElement()
        {
            string dt = tbT.Text;

            if (!byte.TryParse(tbS.Text, out byte sz)) sz = 0;

            if (!byte.TryParse(tbY.Text, out byte py)) py = 0;
            if (!byte.TryParse(tbX.Text, out byte px)) px = 0;

            AColor clr = new AColor(clrBox.GeBoxColor());

            string customName = _textBoxCustomNamePageEl.Text;
            AbstrPageEl p_out = new PageString(
                px, py,
                clr, sz, dt);
            p_out.CustomName = customName;

            return p_out;
        }
    }
}
