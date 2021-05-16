using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

using Lib;

namespace Lib.HelpingClass
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

            PageContainer = new DockPanel();
            ((DockPanel)PageContainer).LastChildFill = false;
            PageContainer.Height = _fullHeight;
            PageContainer.AllowDrop = true;

            PageContainer.Children.Add(_buttonExpand);
            PageContainer.Children.Add(_textBoxCustomNamePageEl);
            PageContainer.Children.Add(
                UIGenerateHelping.NewGridSplitter(10, PageContainer.Background));

            PageContainer.MouseLeftButtonDown += container_MouseDown;
            PageContainer.Drop += (s, e) => RaiseDrop(this, e.Data.GetData(typeof(AbstrUIBase)));
        }
        #region Events
        private void container_MouseDown(object sender, EventArgs e)
        {
            DataObject data = new DataObject(typeof(AbstrUIBase), this);
            DragDrop.DoDragDrop(PageContainer, data, DragDropEffects.Move);
        }
        #endregion
        #region Generic Functions
        public override AbstrPageEl CompileElement() {
            return null; 
        }

        public override void SetID(int id) 
        {
            _buttonExpand.Content = id.ToString();
            ID = id;
        }
        #endregion
        #region Expand
        public void SetExpand(bool expand)
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
            PageContainer.Height = _isExpand ? _fullHeight : _minHeight;

        } 
        private void ExpandButtonClick(object sender, EventArgs eventArgs) 
        { SetExpand(!_isExpand); }

        private void _hidePageElSetting() 
        {
            foreach (UIElement child in PageContainer.Children)
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
            foreach (UIElement child in PageContainer.Children)
            {
                if (child.Uid == pageElName)
                {
                    child.Visibility = Visibility.Hidden;
                    continue;
                }
                child.Visibility = Visibility.Visible;
            }
        }
        #endregion
    }
}
