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
        protected Button _buttonExpand;
        protected TextBox _textBoxCustomNamePageEl;
        protected const int _customNamePageElWidth = 512;

        protected bool _isExpand;
        protected int _fullHeight;
        protected int _minHeight;

        protected string _customNamePageEl;

        protected const string pageElName  = "PageElStr";
        protected const string pageElIndex = "PageElID";

        public UIBaseEl(int Height, AbstrPageEl Pel) : base (Pel.GetNameEl())
        {
            _fullHeight = Height;
            _minHeight = 25;
            _isExpand = true;

            _buttonExpand = new Button();
            _buttonExpand.Uid = pageElIndex;
            _buttonExpand.Width = 20;
            _buttonExpand.Click += ExpandButtonClick;

            _textBoxCustomNamePageEl = new TextBox();
            _textBoxCustomNamePageEl.Uid = pageElName;
            _textBoxCustomNamePageEl.Width = 0;
            _textBoxCustomNamePageEl.Visibility = Visibility.Hidden;
            _textBoxCustomNamePageEl.Text = _customNamePageEl;

            ExpandContainer = new DockPanel();
            ((DockPanel)ExpandContainer).LastChildFill = false;
            ExpandContainer.Height = _fullHeight;
            ExpandContainer.AllowDrop = true;

            ExpandContainer.Children.Add(_buttonExpand);
            ExpandContainer.Children.Add(_textBoxCustomNamePageEl);
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
            _buttonExpand.Content = id.ToString();
            ID = id;
        }

        public void Expand(bool expand)
        {
            if (!expand)
            {
                _hidePageElSetting();
                _textBoxCustomNamePageEl.Width = _customNamePageElWidth;
            }
            else
            {
                _showPageElSetting();
                _textBoxCustomNamePageEl.Width = 0;
            }

            _isExpand = expand;
            ExpandContainer.Height = _isExpand ? _fullHeight : _minHeight;

        } 
        private void ExpandButtonClick(object sender, EventArgs eventArgs) 
        {

            if (_isExpand)
            {
                _hidePageElSetting();
                _textBoxCustomNamePageEl.Width = _customNamePageElWidth;
            }
            else
            {
                _showPageElSetting();
                _textBoxCustomNamePageEl.Width = 0;
            }

            _isExpand = !_isExpand;
            ExpandContainer.Height = _isExpand ? _fullHeight : _minHeight;
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
