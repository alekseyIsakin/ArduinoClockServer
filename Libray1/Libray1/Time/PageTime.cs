using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Lib;
using Lib.HelpingClass;

namespace Lib.Time 
{ 
    public class PageTime : PageEl
    {
        public new const byte ID = (int)Elements.TIME;
        public new const string Name = "Time";

        public override byte GetTypeEl()
        { return ID; }
        public override string GetNameEl()
        { return Name; }

        private byte _size;

        #region Serialize
        const byte MaxSize = 15;

        public bool Hour = true;
        public bool Minute = true;
        public bool Second = false;

        public AColor TextColor;
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
        #endregion

        public PageTime() : this(0,0, AColors.WHITE, 5) { }

        public PageTime(byte x, byte y, AColor clr, byte sz)
        {
            SetPos(x, y);
            TextColor = clr;
            Size = sz;
            CustomName = Name;
        }
        public List<byte> GetTime() 
        {
            List<byte> l_out = new List<byte>();

            byte _hour = Hour ? (byte)(System.DateTime.Now.Hour) : (byte)31;
            byte _minute = Minute ? (byte)(System.DateTime.Now.Minute) : (byte)63;
            byte _second = Second ? (byte)(System.DateTime.Now.Second) : (byte)63;

            l_out.Add(0);
            l_out.Add(0);
            l_out.Add(Size);

            // 2^5 > Hour >= 0
            l_out[0] += (byte)(_hour & 31);
            // 0b_0000_0000_0001_1111

            // 2^6 > Minute >= 0
            l_out[0] += (byte)((_minute & 3) << 5); // 0b_000011
            // 0b_0000_0000 0b_0110_0000
            l_out[1] += (byte)((_minute & 60) >> 2); // 0b_111100
            // 0b_0000_1111 0b_0000_0000

            // 2^6 > Second >= 0
            l_out[1] += (byte)((_second & 7) << 4); // 0b_001111
            // 0b_0111_0000 0b_0000_0000

            l_out[2] += (byte)((_second & 56) << 1); // 0b_110000
            // 0b_0111_0000
            //     ttt ssss
            return l_out;
        }

        public List<byte> GetByteColor()
        {
            return TextColor.GetByteColor();
        }

        public override List<byte> GenSendData()
        {
            List<byte> lout = new List<byte>();

            lout.Add(GetTypeEl());
            lout.AddRange(GetSendPos());
            lout.AddRange(GetByteColor());
            lout.AddRange(GetTime());

            lout.Add(0x00);
            return lout;
        }
    }
}
