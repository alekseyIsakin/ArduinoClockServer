using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArdClock.src.ArdPage.HelpingClass;

namespace ArdClock.src.ArdPage.PageElements
{
    class PageString : PageEl
    {
        public override byte GetTypeEl()
        { return 65; }

        enum StrPageEl
        {
            Position = 1,
            Color,
            Size,
            Data,
        }

        public AColor TextColor;
        public byte Size;

        public string Data;

        public PageString() : this(0,0, AColors.WHITE, 5, "string")
        { }

        public PageString(byte x, byte y, AColor clr, byte sz, string str)
            : base(x, y)
        {
            this.TextColor = clr;
            this.Size = sz;
            this.Data = str;
        }

        public List<byte> GetByteColor()
        {
            return TextColor.GetByteColor();
        }

        public override List<byte> GenSendData()
        {
            List<byte> lout = new List<byte>();

            lout.Add(GetTypeEl());
            lout.AddRange(GeSendtPos());
            lout.AddRange(GetByteColor());
            lout.Add(Size);

            foreach (Char chr in Data)
            {
                lout.Add((byte)chr);
            }

            lout.Add(0x00);
            return lout;
        }
    }

}
