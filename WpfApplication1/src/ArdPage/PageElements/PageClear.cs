using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using BaseLib;

namespace ArdClock.ArdPage.PageElements
{
    class PageClear : AbstrPageEl
    {
        static PageClear() 
        { ID = 127; }
        public override byte GetTypeEl()
        { return PageClear.ID; }

        public override List<byte> GenSendData() 
        {
            List<byte> arrOut = new List<byte>();

            arrOut.Add((byte)127);
            arrOut.Add(0);

            return arrOut;
        }
    }
}
