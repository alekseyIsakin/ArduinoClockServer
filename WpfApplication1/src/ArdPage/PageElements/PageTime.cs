using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArdClock.src.ArdPage.HelpingClass;

namespace ArdClock.src.ArdPage.PageElements
{
    class PageTime : PageEl
    {
        public override byte GetTypeEl()
        { return 66; }

        const byte MaxSize = 15;

        public bool Hour = true;
        public bool Minute= true;
        public bool Second= false;

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

        public PageTime() : this(0,0, AColors.WHITE, 5) { }

        public PageTime(byte x, byte y, AColor clr, byte sz)
        {
            SetPos(x, y);
            TextColor = clr;
            Size = sz;
        }

        public override List<byte> GenSendData()
        {
            byte _hour = Hour ? (byte)(System.DateTime.Now.Hour) : (byte)31;
            byte _minute = Minute ? (byte)(System.DateTime.Now.Minute) : (byte)63;
            byte _second = Second ? (byte)(System.DateTime.Now.Second) : (byte)63;

            byte byte_size = Size;

            TimeSetting[0] = 0;
            TimeSetting[1] = 0;

            // 2^5 > Hour >= 0
            TimeSetting[0] += (byte)(_hour & 31);
            // 0b_0000_0000_0001_1111

            // 2^6 > Minute >= 0
            TimeSetting[0] += (byte)((_minute & 3) << 5); // 0b_000011
            // 0b_0000_0000 0b_0110_0000
            TimeSetting[1] += (byte)((_minute & 60) >> 2); // 0b_111100
            // 0b_0000_1111 0b_0000_0000

            // 2^6 > Second >= 0
            TimeSetting[1] += (byte)((_second & 7) << 4); // 0b_001111
            // 0b_0111_0000 0b_0000_0000

            byte_size += (byte)((_second & 56) << 1); // 0b_110000
            // 0b_0111_0000
            //     ttt ssss

            List<byte> lout = new List<byte>();

            lout.Add(GetTypeEl());
            lout.AddRange(GeSendtPos());
            lout.AddRange(TextColor.GetByteColor());
            lout.Add(TimeSetting[0]);
            lout.Add(TimeSetting[1]);
            lout.Add(byte_size);

            lout.Add(0x00);
            return lout;
        }
    }
}
