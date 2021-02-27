using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using BaseLib;

namespace ArdClock.src.ArdPage.PageElements
{
    public class PageEl : AbstrPageEl
    {
        public override byte GetTypeEl()
        { return 0; }

        public override void SetID(int id)
        { ID = id; }

        public byte X { get; private set; }
        public byte Y { get; private set; }

        public PageEl() 
        {
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
            lout.Add((byte)(X / 2));
            lout.Add((byte)(Y / 2));
            return lout;
        }

        public override List<byte> GenSendData()
        {
            return new List<byte>();
        }
    }
}
