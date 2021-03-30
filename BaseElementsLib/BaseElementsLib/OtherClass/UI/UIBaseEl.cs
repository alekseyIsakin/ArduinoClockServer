using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

using BaseLib;

namespace BaseLib.HelpingClass
{
    public class UIBaseEl : AbstrUIBase
    {
        protected Button buttonExpand;
        protected TextBox textBoxCustomNamePageEl;
        protected int customNamePageElWidth = 512;

        protected bool isExpand;
        protected int fullHeight;
        protected int minHeight;

        protected string customNamePageEl;

        protected const string pageElName  = "PageElStr";
        protected const string pageElIndex = "PageElID";

        public UIBaseEl(int Height, AbstrPageEl Pel) : base (Pel.GetNameEl())
        {
            fullHeight = Height;
            minHeight = 25;
            isExpand = true;

            buttonExpand = new Button();
            buttonExpand.Uid = pageElIndex;
            buttonExpand.Width = 20;
            buttonExpand.Click += Expand;

            textBoxCustomNamePageEl = new TextBox();
            textBoxCustomNamePageEl.Uid = pageElName;
            textBoxCustomNamePageEl.Width = 0;
            textBoxCustomNamePageEl.Visibility = Visibility.Hidden;
            textBoxCustomNamePageEl.Text = customNamePageEl;

            ExpandContainer = new DockPanel();
            ((DockPanel)ExpandContainer).LastChildFill = false;
            ExpandContainer.Height = fullHeight;
            ExpandContainer.AllowDrop = true;

            ExpandContainer.Children.Add(buttonExpand);
            ExpandContainer.Children.Add(textBoxCustomNamePageEl);
            ExpandContainer.Children.Add(
                UIGenerateHelping.NewGridSplitter(10, ExpandContainer.Background));

            ExpandContainer.MouseLeftButtonDown += container_MouseDown;
            ExpandContainer.Drop += (s, e) => RaiseDrop(this, e.Data.GetData(typeof(AbstrUIBase)));
        }

        private void container_MouseDown(object sender, EventArgs e)
        {
            DataObject data = new DataObject(typeof(AbstrUIBase), this);
            DragDrop.DoDragDrop(ExpandContainer, data, DragDropEffects.Move);
        }

        public override AbstrPageEl CompileElement() {
            return null; 
        }

        public override void SetID(int id) 
        {
            buttonExpand.Content = id.ToString();
            ID = id;
        }

        private void Expand(object sender, EventArgs eventArgs) 
        {

            if (isExpand)
            {
                _hidePageElSetting();
                textBoxCustomNamePageEl.Width = customNamePageElWidth;
            }
            else
            {
                _showPageElSetting();
                textBoxCustomNamePageEl.Width = 0;
            }

            isExpand = !isExpand;
            ExpandContainer.Height = isExpand ? fullHeight : minHeight;
        }

        private void _hidePageElSetting() 
        {
            foreach (UIElement child in ExpandContainer.Children)
            {
                if (child.Uid == pageElIndex || child.Uid == pageElName) 
                {
                    child.Visibility = Visibility.Visible;
                    continue;
                }
                child.Visibility = Visibility.Hidden;
            }
        }
        private void _showPageElSetting()
        {
            foreach (UIElement child in ExpandContainer.Children)
            {
                if (child.Uid == pageElName)
                {
                    child.Visibility = Visibility.Hidden;
                    continue;
                }
                child.Visibility = Visibility.Visible;
            }
        }
    }
}
