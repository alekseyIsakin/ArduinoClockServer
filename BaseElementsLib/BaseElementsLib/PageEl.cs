using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BaseLib
{
    public class PageEl : AbstrPageEl
    {
        public byte X { get; private set; }
        public byte Y { get; private set; }
        public override byte GetTypeEl()
        { return ID; }
        public override string GetNameEl()
        { return Name; }
        public PageEl() 
        {
            CustomName = Name;
            X = 0; Y = 0; 
        }

        public PageEl(byte x, byte y)
        {
            SetPos(x, y);
        }

        public void SetPos(byte x, byte y)
        {
            this.X = x;
            this.Y = y;
        }

        public List<byte> GeSendtPos()
        {
            List<byte> lout = new List<byte>();
            lout.Add((byte)(X));
            lout.Add((byte)(Y));
            return lout;
        }

        public override List<byte> GenSendData()
        {
            return new List<byte>();
        }
    }
}
