using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArdClock.src.XMLLoader
{
    class XMLDefines
    {
        public class XMLTag
        {
            public const string Pages = "Pages";
            public const string Page = "Page";
            public const string PageEl = "PageEl";
        }

        public class XMLPageAttr
        {
            public const string Name = "Name";
            public const string ID = "ID";
        }

        public class XMLBaseElTag 
        {
            public const string Position = "Position";
        }

        public class XMLBaseElAttr
        {
            public const string TypeEl = "TPageEl";
            public const string PosX = "Pos_x";
            public const string PosY = "Pos_y";
        }

        public class XMLStringTag : XMLBaseElTag
        {
            public const string Color = "Color";
            public const string Size = "Size";
            public const string Data = "Data";
        }
        public class XMLStringAttr : XMLBaseElAttr
        {
            public const string ColorValue = "Value";
            public const string SizeValue = "Value";
            public const string Data = "Value";
        }

        public class XMLTimeTag : XMLStringTag
        { }

        public class XMLTimeAttr : XMLStringAttr
        {
            public const string DataTmFlag = "Flags";
        }
    }
}
