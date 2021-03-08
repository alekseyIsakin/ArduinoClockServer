using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using BaseLib.HelpingClass;

using Lib.String;

namespace Lib.CustString
{
    public class PageCustomString : PageString
    {
        public new const byte ID = 65;
        public new const string Name = "String";
        public override byte GetTypeEl()
        { return ID; }
        public override string GetNameEl()
        { return Name; }

        public PageCustomString(byte x, byte y, AColor clr, byte sz, string str) 
            : base(x, y, clr, sz, str)
        { }
    }

}
