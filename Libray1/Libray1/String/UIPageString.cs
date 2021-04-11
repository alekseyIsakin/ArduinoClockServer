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

namespace Lib.String
{
    public class UIPageString : UIBaseEl
    {
        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
            Content = PageContainer;
        }
        public UIPageString(AbstrPageEl pEl)
            : base(60, pEl)
        {
            PageString ps = (PageString)pEl;
            SetID (PageString.ID);

            _customNamePageEl = ps.CustomName;
            _textBoxCustomNamePageEl.Text = _customNamePageEl;

            // Интерфейс для настройки позиции
            Label lbl_pos = new Label();
            TextBox tbX = new TextBox();
            TextBox tbY = new TextBox();

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
            UIAcolorBox clrBox = new UIAcolorBox(
                ps.TextColor.GetColor());

            // Размер
            Label lbl_size = new Label();
            TextBox tbS = new TextBox();

            lbl_size.Content = "Размер";
            tbS.Text = ps.Size.ToString();

            tbS.Width = 25;
            tbS.Height = 23;
            //

            // Текст
            TextBox tbT = new TextBox();

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
            int id = 0;
            string dt = "";
            AColor clr = AColors.WHITE;
            int px = 0;
            int py = 0;
            int sz = 0;
            string customName = "None";

            foreach (UIElement ch in PageContainer.Children) 
            {
                switch (ch.Uid) 
                {
                    case "tbT":
                        dt = ((TextBox)ch).Text;
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
                        { clr = new AColor(((UIAcolorBox)ch).GeBoxColor()); }
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
            AbstrPageEl p_out;

            p_out = new PageString(
                (byte)px, (byte)py, 
                clr, 
                (byte)sz,
                dt);
            p_out.CustomName = customName;

            return p_out;
        }
    }
}
