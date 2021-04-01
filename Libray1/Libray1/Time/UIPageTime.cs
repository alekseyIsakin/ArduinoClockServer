﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

using BaseLib;
using BaseLib.HelpingClass;

namespace Lib.Time
{
    class UIPageTime : UIBaseEl
    {
        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
            Content = ExpandContainer;
        }
        public UIPageTime(AbstrPageEl pEl)
            : base(47, pEl)
        {
            PageTime pt = (PageTime)pEl;
            SetID(PageTime.ID);

            _customNamePageEl = pt.CustomName;
            _textBoxCustomNamePageEl.Text = _customNamePageEl;

            // Интерфейс для настройки позиции
            Label lbl_pos = new Label();
            TextBox tbX = new TextBox();
            TextBox tbY = new TextBox();

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

            UIAcolorBox clrBox = new UIAcolorBox(
                pt.TextColor.GetColor());
            //

            // Размер
            Label lbl_size = new Label();
            TextBox tbS = new TextBox();

            lbl_size.Content = "Размер";
            lbl_size.VerticalAlignment = VerticalAlignment.Center;

            tbS.Text = pt.Size.ToString();

            tbS.Width = 25;
            tbS.Height = 23;
            //

            // Флаги
            StackPanel spFlasgs = new StackPanel();

            CheckBox cbSecond = new CheckBox();
            CheckBox cbMinut = new CheckBox();
            CheckBox cbHour = new CheckBox();

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
            
            ExpandContainer.Children.Add(lbl_pos);
            ExpandContainer.Children.Add(tbX);
            ExpandContainer.Children.Add(
                UIGenerateHelping.NewGridSplitter(10, ExpandContainer.Background));
            ExpandContainer.Children.Add(tbY);

            ExpandContainer.Children.Add(
                UIGenerateHelping.NewGridSplitter(10, ExpandContainer.Background));

            ExpandContainer.Children.Add(clrBox);

            ExpandContainer.Children.Add(
                UIGenerateHelping.NewGridSplitter(10, ExpandContainer.Background));

            ExpandContainer.Children.Add(lbl_size);
            ExpandContainer.Children.Add(tbS);

            ExpandContainer.Children.Add(
                UIGenerateHelping.NewGridSplitter(10, ExpandContainer.Background));

            ExpandContainer.Children.Add(spFlasgs);

            clrBox.Uid = "clrBox";
            tbX.Uid = "tbX";
            tbY.Uid = "tbY";
            tbS.Uid = "tbS";
            spFlasgs.Uid = "spF";
            cbSecond.Uid = "cbS";
            cbMinut.Uid = "cbM";
            cbHour.Uid = "cbH";
        }
        public override AbstrPageEl CompileElement() 
        {
            int id = 0;
            bool sec = false;
            bool min = false;
            bool hour = false;
            AColor clr = AColors.WHITE;
            int px = 0;
            int py = 0;
            int sz = 0;
            string customName = "None";


            foreach (UIElement ch in ExpandContainer.Children) 
            {
                switch (ch.Uid) 
                {
                    case "spF":
                        StackPanel sp = (StackPanel)ch;

                        sec = (bool)((CheckBox)sp.Children[0]).IsChecked;
                        min = (bool)((CheckBox)sp.Children[1]).IsChecked;
                        hour = (bool)((CheckBox)sp.Children[2]).IsChecked;
                        break;

                    case "tbS":
                        if (int.TryParse(((TextBox)ch).Text, out sz))
                            sz = (sz & byte.MaxValue);
                        else
                            sz = 0;
                        break;
                    case "tbY":
                        if (int.TryParse(((TextBox)ch).Text, out py))
                            py = (py & byte.MaxValue);
                        else
                            py = 0;
                        break;
                    case "tbX":
                        if (int.TryParse(((TextBox)ch).Text, out px))
                            px = (px & byte.MaxValue);
                        else
                            px = 0;
                        break;
                    case "clrBox":
                        try
                        { clr = new AColor((ch as UIAcolorBox).GeBoxColor()); }
                        catch
                        { }
                        break;
                    case pageElIndex:
                        if (!int.TryParse(
                            ((ch as ContentControl).Content as string), out id))
                        { id = 0; }
                        break;
                    case pageElName:
                        customName = (ch as TextBox).Text;
                        break;
                }
            }

            AbstrPageEl p_out = new PageTime(
                (byte)px, (byte)py, 
                clr, 
                (byte)sz);

            ((PageTime)p_out).Second = sec;
            ((PageTime)p_out).Minute = min;
            ((PageTime)p_out).Hour = hour;
            p_out.CustomName = customName;

            return p_out;
        }
    }
}
