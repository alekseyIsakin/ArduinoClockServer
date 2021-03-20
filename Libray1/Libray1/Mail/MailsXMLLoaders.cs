using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

using BaseLib;
using BaseLib.Xml;
using BaseLib.HelpingClass;

namespace Lib.Mails
{
    public static partial class TimeXmlBehavior
    {
        static public void LoadSecret() 
        { }
        static public AbstrPageEl ReadXMLPageTime(XmlNode nd_el)
        {
            PageEl out_pageEl = new PageEl();
            string pos_x = "0", pos_y = "0",
                   clr_hex = "0x", sz = "0", dt_str = "";

            try
            {

                foreach (XmlNode nd_string_par in nd_el)
                {
                    switch (nd_string_par.Name)
                    {
                        case (XMLDefines.XMLBaseElTag.Position):

                            pos_x = nd_string_par.Attributes.GetNamedItem(
                                XMLDefines.XMLBaseElAttr.PosX).Value;
                            pos_y = nd_string_par.Attributes.GetNamedItem(
                                XMLDefines.XMLBaseElAttr.PosY).Value;
                            break;

                        case (XMLDefines.XMLBaseElTag.Color):
                            clr_hex = nd_string_par.Attributes.GetNamedItem(
                                XMLDefines.XMLBaseElAttr.ColorValue).Value;
                            break;

                        case (XMLDefines.XMLBaseElTag.Size):
                            sz = nd_string_par.Attributes.GetNamedItem(
                                XMLDefines.XMLBaseElAttr.SizeValue).Value;
                            break;

                        case (XMLDefines.XMLBaseElTag.Data):
                            dt_str = nd_string_par.Attributes.GetNamedItem(
                                XMLDefines.XMLBaseElAttr.Data).Value;
                            break;
                    }
                }
            }
            catch
            {
            }

            try
            {
                Byte b_pos_x, b_pos_y, b_sz;
                AColor color;

                b_pos_x = Convert.ToByte(pos_x);
                b_pos_y = Convert.ToByte(pos_y);
                b_sz = Convert.ToByte(sz);

                color = new AColor(clr_hex);
                out_pageEl = new PageMails(b_pos_x, b_pos_y, color, b_sz);
            }
            catch (Exception e)
            {
            }
            return out_pageEl;
        }
    }
}
