using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BaseLib
{
    public abstract class AbstrPageEl
    {
        public static readonly byte ID;
        public const string Name = "AbstrPageEl";
        public abstract List<byte> GenSendData();
        public abstract byte GetTypeEl();
        public abstract string GetNameEl();
    }
}
