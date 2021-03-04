using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BaseLib.Xml
{
    public class XMLDefines
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
            public const string Color = "Color";
            public const string Size = "Size";
            public const string Data = "Data";
        }

        public class XMLBaseElAttr
        {
            public const string TypeEl = "TPageEl";
            public const string PosX = "Pos_x";
            public const string PosY = "Pos_y";
            public const string ColorValue = "Value";
            public const string SizeValue = "Value";
            public const string Data = "Value";
        }
    }
}
