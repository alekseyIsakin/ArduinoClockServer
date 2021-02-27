using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

using ArdClock.src.ArdPage.PageElements;

using BaseLib;

namespace ArdClock.src.UIGenerate
{
    public class UIBaseEl : AbstrUIBase
    {
        protected ContextMenu DPContextMenu;
        protected Label labl_ID;

        public UIBaseEl(int Height)
        {
            labl_ID = new Label();
            labl_ID.VerticalAlignment = VerticalAlignment.Center;
            labl_ID.Uid = "lblID";

            DPContextMenu = new ContextMenu();
            MenuItem mi = new MenuItem();

            mi.Header = "Удалить";
            mi.Click += RaiseDelClick;

            DPContextMenu.Items.Add(mi);

            Container = new DockPanel();
            ((DockPanel)Container).LastChildFill = false;
            Container.Height = Height;

            Container.ContextMenu = DPContextMenu;
            Container.AllowDrop = true;

            Container.Children.Add(labl_ID);

            Container.Children.Add(
                UIGenerateHelping.NewGridSplitter(10, Container.Background));

            Container.MouseLeftButtonDown += container_MouseDown;
            Container.Drop += (s, e) => RaiseDrop(this, e.Data.GetData(typeof(AbstrUIBase)));
        }

        private void container_MouseDown(object sender, EventArgs e)
        {
            DataObject data = new DataObject(typeof(AbstrUIBase), this);
            DragDrop.DoDragDrop(Container, data, DragDropEffects.Move);
        }

        public override AbstrPageEl CompileElement() {
            return null; 
        }

        public override void SetID(int id) 
        { 
            if (labl_ID != null)
            {
                labl_ID.Content = id.ToString();
                ID = id;
            }
        }
    }
}
