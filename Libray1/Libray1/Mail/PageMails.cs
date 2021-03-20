using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using BaseLib;
using BaseLib.HelpingClass;

namespace Lib.Mails
{ 
    public class PageMails : PageEl
    {
        public new const byte ID = (int)Elements.MAIL;
        public new const string Name = "Mail";

        public override byte GetTypeEl()
        { return ID; }
        public override string GetNameEl()
        { return Name; }

        const byte MaxSize = 15;

        public AColor TextColor;
        private byte _size;
        public byte Size 
        {
            get
            { return _size; }
            set 
            {
                if (value > MaxSize) _size = MaxSize;
                else _size = value;
            }
        }

        public byte[] TimeSetting = new byte[2];

        public PageMails() : this(0,0, AColors.WHITE, 5) { }

        public PageMails(byte x, byte y, AColor clr, byte sz)
        {
            SetPos(x, y);
            TextColor = clr;
            Size = sz;
        }

        public override List<byte> GenSendData()
        {
            List<byte> lout = new List<byte>();

            lout.Add(GetTypeEl());
            lout.AddRange(GeSendtPos());
            lout.AddRange(TextColor.GetByteColor());

            lout.Add(0x00);
            return lout;
        }
    }
}
