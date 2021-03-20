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

namespace Lib.Mails
{
    class UIPageMails : UIBaseEl
    {
        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
            Content = Container;
        }
        public UIPageMails(AbstrPageEl pEl)
            : base(47, pEl)
        {
            PageMails pt = (PageMails)pEl;
            SetID(PageMails.ID);

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

            //

            labl_ID.Content = ID.ToString();
            //
            
            Container.Children.Add(lbl_pos);
            Container.Children.Add(tbX);
            Container.Children.Add(
                UIGenerateHelping.NewGridSplitter(10, Container.Background));
            Container.Children.Add(tbY);

            Container.Children.Add(
                UIGenerateHelping.NewGridSplitter(10, Container.Background));

            Container.Children.Add(clrBox);

            Container.Children.Add(
                UIGenerateHelping.NewGridSplitter(10, Container.Background));

            Container.Children.Add(lbl_size);
            Container.Children.Add(tbS);

            clrBox.Uid = "clrBox";
            tbX.Uid = "tbX";
            tbY.Uid = "tbY";
            tbS.Uid = "tbS";
        }
        public override AbstrPageEl CompileElement() 
        {
            int id = 0;
            AColor clr = AColors.WHITE;
            int px = 0;
            int py = 0;
            int sz = 0;
            


            foreach (UIElement ch in Container.Children) 
            {
                switch (ch.Uid) 
                {
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
                    case "lblID":
                        if (!int.TryParse(
                            ((ch as Label).Content as string), out id))
                        { id = 0; }
                        break;
                }
            }

            AbstrPageEl p_out = new PageMails(
                (byte)px, (byte)py, 
                clr, 
                (byte)sz);

            return p_out;
        }
    }
}
