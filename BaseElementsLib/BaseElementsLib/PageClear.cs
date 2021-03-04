using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BaseLib
{
    public class PageClear : AbstrPageEl
    {
        public new const byte ID = 127;
        public new const string Name = "PageClear";
        public override byte GetTypeEl()
        { return ID; }
        public override string GetNameEl()
        { return Name; }
        public override List<byte> GenSendData()
        {
            List<byte> arrOut = new List<byte>();

            arrOut.Add((byte)127);
            arrOut.Add(0);

            return arrOut;
        }
    }
}
