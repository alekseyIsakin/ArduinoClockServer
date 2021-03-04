using System;
using System.Windows;
using System.Windows.Controls;

namespace BaseLib
{
    public class DragDropArgs : EventArgs
    {
        public readonly object drop;

        public DragDropArgs(object drop) 
        { this.drop = drop; }
    }

    public abstract class AbstrUIBase : UserControl
    {
        protected override void OnInitialized(EventArgs e)
        { base.OnInitialized(e); }
        public int ID { get; protected set; }

        public abstract void SetID(int id);

        public Panel Container { get; protected set; }

        public event EventHandler DelClick;
        public event EventHandler Drop;

        public abstract AbstrPageEl CompileElement();

        protected void RaiseDelClick(object sender, EventArgs e)
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
