using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace BaseLib.HelpingClass
{
    public class AColor
    {
        public byte block1 { get; private set; }
        // #### ####
        // ^^^^
        public byte block2 { get; private set; }
        // #### ####
        //      ^^^^
        public AColor() 
        { SetFromInt(0x0000); }

        public AColor(UInt16 uint_16)
        { SetFromInt(uint_16); }

        public AColor(string hex)
        { SetFromHex(hex); }

        public AColor(Color clr)
        { SetFromColor(clr); }

        public void SetFromInt(int dt)
        {
            block2 = (byte)(dt & byte.MaxValue);
            block1 = (byte)((dt - (dt & byte.MaxValue)) >> 8);
        }

        public void SetFromHex(string s)
        {
            s = s.Replace("0x", ""); // 0xff_ff => ff_ff
            s = s.Replace("_", "");  //   ff_ff => ffff

            int dt = int.Parse(s, System.Globalization.NumberStyles.HexNumber);
            SetFromInt(dt);
        }

        public void SetFromColor(Color clr)
        {
            //    block1    block2 
            // 0b_1111_1111_1111_1111
            //    rrrr rggg gggb bbbb

            byte d1 = 56;  // 0b_111000 3bit for Green
            byte d2 = 7;   // 0b_000111 3bit for Green

            byte R, G, B;
            block1 = 0;
            block2 = 0;

            R = (byte)(clr.R / 8);
            G = (byte)(clr.G / 4);
            B = (byte)(clr.B / 8);

            block1 =  (byte)(R << 3);
            block1 += (byte)((G & d1) >> 3);
            block2 =  (byte)((G & d2) << 5);
            block2 += B;
        }

        public Color GetColor() 
        {
            byte R, G, B;
            
            //    block1    block2 
            // 0b_1111_1111_1111_1111
            //    rrrr rggg gggb bbbb
            
            byte d1 = 248;   // 0b_1111_1000 5bit for Red 
            byte d2 = 7;     // 0b_0000_0111 3bit for Green
            byte d3 = 224;   // 0b_1110_0000 3bit for Green
            byte d4 = 31;    // 0b_0001_1111 5bit for Blue

            R = (byte)((block1 & d1) >> 3);
            R *= 8; // 5+3 = 8bit

            G = (byte)((block1 & d2) << 3);
            G += (byte)((block2 & d3) >> 5);
            G *= 4; //6+2 = 8bit

            B = (byte)(block2 & d4);
            B *= 8; // 5+3 = 8bit

            //System.Windows.MessageBox.Show(String.Format("{0} {1} {2}", R, G, B));
            return Color.FromRgb(R,G,B);
        }

        

        public string ToHex() 
        {
            string s_out = "0x";
            string tmp_s = "";

            tmp_s = block1.ToString("X");

            while (s_out.Length < 4 - tmp_s.Length)
                s_out += "0";
            s_out += tmp_s;

            tmp_s = block2.ToString("X");

            while (s_out.Length < 6 - tmp_s.Length)
                s_out += "0";
            s_out += tmp_s;

            return s_out;
        }

        public List<byte> GetByteColor()
        {
            List<byte> lout = new List<byte>();

            lout.Add((byte)(block1 / 2));
            // #### ####
            // ^^^^
            lout.Add((byte)(block2 / 2));
            // #### ####
            //      ^^^^

            return lout;
        }
    }
}
