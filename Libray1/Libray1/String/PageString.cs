﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Lib;
using Lib.HelpingClass;

namespace Lib.String
{
    public class PageString : PageEl
    {
        public new const byte ID = (int)Elements.STRING;
        public new const string Name = "String";
        public override byte GetTypeEl()
        { return ID; }
        public override string GetNameEl()
        { return Name; }


        #region Serialize
        public AColor TextColor;
        public byte Size;
        public string Data; 
        #endregion

        public PageString() : this(0, 0, AColors.WHITE, 5, "string")
        { }

        public PageString(byte x, byte y, AColor clr, byte sz, string str)
            : base(x, y)
        {
            this.TextColor = clr;
            this.Size = sz;
            this.Data = str;
            CustomName = Name;
        }

        public List<byte> GetByteColor()
        {
            return TextColor.GetByteColor();
        }

        public override List<byte> GenSendData()
        {
            List<byte> lout = new List<byte>();

            lout.Add(GetTypeEl());
            lout.AddRange(GetSendPos());
            lout.AddRange(GetByteColor());
            lout.Add(Size);

            foreach (Char chr in Data)
            {
                lout.Add((byte)chr);
            }

            lout.Add(0x00);
            return lout;
        }
    }

}
