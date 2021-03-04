using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BaseLib.HelpingClass
{
    public sealed class AColors 
    {
        public static Dictionary<string, AColor> AColorsDict { get; private set; }

        public static readonly AColor BLACK = new AColor(0x0000);
        public static readonly AColor BLUE = new AColor(0x001F);
        public static readonly AColor RED = new AColor(0xF800);
        public static readonly AColor GREEN = new AColor(0x07E0);
        public static readonly AColor CYAN = new AColor(0x07FF);
        public static readonly AColor MAGENTA = new AColor(0xF81F);
        public static readonly AColor YELLOW = new AColor(0xFFE0);
        public static readonly AColor WHITE = new AColor(0xFFFF);

        static AColors() 
        {
            AColorsDict = new Dictionary<string, AColor>();

            AColorsDict.Add("BLACK", new AColor(0x0000));
            AColorsDict.Add("BLUE", new AColor(0x001F));
            AColorsDict.Add("RED", new AColor(0xF800));
            AColorsDict.Add("GREEN", new AColor(0x07E0));
            AColorsDict.Add("CYAN", new AColor(0x07FF));
            AColorsDict.Add("MAGENTA", new AColor(0xF81F));
            AColorsDict.Add("YELLOW", new AColor(0xFFE0));
            AColorsDict.Add("WHITE", new AColor(0xFFFF));
        }
    }
}
