using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArdClock.src
{
    class NIcon : IDisposable
    {
        public delegate void NIconHandler(object sender, EventArgs e);

        public event NIconHandler Click;
        public event NIconHandler DoubleClick;
        public event NIconHandler ContextMenuClose;
        public event NIconHandler ContextMenuConnect;

        public System.Windows.Forms.NotifyIcon notifyIcon {get; private set;}

        public NIcon(System.Windows.Media.ImageSource icn) 
        {
            notifyIcon = new System.Windows.Forms.NotifyIcon();
            notifyIcon.Click += notifyIcon_Click;
            notifyIcon.DoubleClick += notifyIcon_DoubleClick;

            SetIcon(false);
            notifyIcon.Visible = true;

            var cm = new System.Windows.Forms.ContextMenuStrip();
            var itClose = new System.Windows.Forms.ToolStripMenuItem("Close");
            var itConnect = new System.Windows.Forms.ToolStripMenuItem("Connect");

            itClose.Click += onClose;
            itConnect.Click += onConnect;

            cm.Items.AddRange(new[] { itClose, itConnect });

            notifyIcon.ContextMenuStrip = cm;
        }

        public void Dispose()
        {
            notifyIcon.Dispose();
        }

        public void notifyIcon_Click(object sender, EventArgs e)
            { Click(this, e); }
        public void notifyIcon_DoubleClick(object sender, EventArgs e)
            { DoubleClick(this, e);}

        public void onClose(object sender, EventArgs e)
            { ContextMenuClose(this, e); }
        public void onConnect(object sender, EventArgs e)
            { ContextMenuConnect(this, e);  }

        public void SetIcon(bool state) 
        {
            notifyIcon.Icon = state ? Properties.Resources.OK_icon :
                                      Properties.Resources.NO_icon;
        }
    }
}
