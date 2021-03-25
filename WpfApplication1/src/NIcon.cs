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
        public event NIconHandler ContextMenuClose;
        public event NIconHandler ContextMenuConnect;

        public System.Windows.Forms.NotifyIcon NotifyIcon {get; private set;}

        public NIcon() 
        {
            NotifyIcon = new System.Windows.Forms.NotifyIcon();
            NotifyIcon.Click += NotifyIcon_Click;

            ToggleIcon(false);
            NotifyIcon.Visible = true;

            var cm = new System.Windows.Forms.ContextMenuStrip();
            var itClose = new System.Windows.Forms.ToolStripMenuItem("Close");
            var itConnect = new System.Windows.Forms.ToolStripMenuItem("Connect");

            itClose.Click += OnClose;
            itConnect.Click += OnConnect;

            cm.Items.AddRange(new[] { itClose, itConnect });

            NotifyIcon.ContextMenuStrip = cm;
        }

        public void Dispose()
        {
            NotifyIcon.Icon = null;
            NotifyIcon.Dispose();
        }

        public void NotifyIcon_Click(object sender, EventArgs e)
            { Click(this, e); }

        public void OnClose(object sender, EventArgs e)
            { ContextMenuClose(this, e); }
        public void OnConnect(object sender, EventArgs e)
            { ContextMenuConnect(this, e);  }

        public void ToggleIcon(bool state) 
        {
            NotifyIcon.Icon = state ? Properties.Resources.OK_icon :
                                      Properties.Resources.NO_icon;
        }
    }
}
