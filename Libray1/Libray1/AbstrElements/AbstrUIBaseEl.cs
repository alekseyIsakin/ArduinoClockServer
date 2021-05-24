using System;
using System.Windows;
using System.Windows.Controls;

namespace Lib
{
    public class DragDropArgs : EventArgs
    {
        public readonly object drop;

        public DragDropArgs(object drop) 
        { this.drop = drop; }
    }

    public abstract class AbstrUIBase : UserControl
    {
        public event EventHandler DelClick;
        public new event EventHandler Drop;

        public readonly string NamePageEl;

        public Panel PageContainer { get; protected set; }

        public int ID { get; protected set; }
        public abstract void SetID(int id);

        public AbstrUIBase(string Name) 
        { NamePageEl = Name; }

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
            Content = PageContainer;
        }

        public abstract AbstrPageEl CompileElement();

        public void RaiseDelClick(object sender, EventArgs e)
        {
            if (DelClick != null)
                DelClick.Invoke(this, e);
        }
        protected void RaiseDrop(object sender, object drop)
        {
            if (Drop != null)
                Drop.Invoke(sender, new DragDropArgs(drop));
        }
    }
}
