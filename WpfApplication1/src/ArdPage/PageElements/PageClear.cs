using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using BaseLib;

namespace ArdClock.src.ArdPage.PageElements
{
    class PageClear : AbstrPageEl
    {
        public override void SetID(int id) { ID = id; }

        public override byte GetTypeEl()
        { return 127; }

        public override List<byte> GenSendData() 
        {
            List<byte> arrOut = new List<byte>();

            arrOut.Add((byte)127);
            arrOut.Add(0);

            return arrOut;
        }
    }
}
