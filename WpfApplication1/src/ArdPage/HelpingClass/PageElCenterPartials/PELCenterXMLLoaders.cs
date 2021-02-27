using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

using ArdClock.src.ArdPage;
using ArdClock.src.UIGenerate;
using ArdClock.src.ArdPage.PageElements;
using ArdClock.src.XMLLoader;

using BaseLib;

namespace ArdClock.src.ArdPage.HelpingClass
{
    public static partial class PageElCenter
    {
        public static AbstrPageEl TryLoadFromXml(int id, XmlNode nd)
        {
            if (HasID(id)) 
                return _funcsXmlLoaders[id](nd);
            return null;
        }
        static private AbstrPageEl ReadLikePageString(XmlNode nd_el)
        {
            PageEl out_pageEl = new PageEl();
            string pos_x = "0", pos_y = "0",
                   clr_hex = "0x", sz = "0", dt_str = "";

            foreach (XmlNode nd_string_par in nd_el)
            {
                switch (nd_string_par.Name)
                {
                    case (XMLDefines.XMLStringTag.Position):

                        pos_x = nd_string_par.Attributes.GetNamedItem(
                            XMLDefines.XMLStringAttr.PosX).Value;
                        pos_y = nd_string_par.Attributes.GetNamedItem(
                            XMLDefines.XMLStringAttr.PosY).Value;
                        break;

                    case (XMLDefines.XMLStringTag.Color):
                        clr_hex = nd_string_par.Attributes.GetNamedItem(
                            XMLDefines.XMLStringAttr.ColorValue).Value;
                        break;

                    case (XMLDefines.XMLStringTag.Size):
                        sz = nd_string_par.Attributes.GetNamedItem(
                            XMLDefines.XMLStringAttr.SizeValue).Value;
                        break;

                    case (XMLDefines.XMLStringTag.Data):
                        dt_str = nd_string_par.Attributes.GetNamedItem(
                            XMLDefines.XMLStringAttr.Data).Value;
                        break;
                }
            }

            try
            {
                Byte b_pos_x, b_pos_y, b_sz;
                AColor color;

                b_pos_x = Convert.ToByte(pos_x);
                b_pos_y = Convert.ToByte(pos_y);
                b_sz = Convert.ToByte(sz);

                color = new AColor(clr_hex);
                out_pageEl = new PageString(b_pos_x, b_pos_y, color, b_sz, dt_str);
            }
            catch (Exception e)
            {
            }
            return out_pageEl;
        }
        static private AbstrPageEl ReadLikePageTime(XmlNode nd_el)
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
                        case (XMLDefines.XMLTimeTag.Position):

                            pos_x = nd_string_par.Attributes.GetNamedItem(
                                XMLDefines.XMLTimeAttr.PosX).Value;
                            pos_y = nd_string_par.Attributes.GetNamedItem(
                                XMLDefines.XMLTimeAttr.PosY).Value;
                            break;

                        case (XMLDefines.XMLTimeTag.Color):
                            clr_hex = nd_string_par.Attributes.GetNamedItem(
                                XMLDefines.XMLTimeAttr.ColorValue).Value;
                            break;

                        case (XMLDefines.XMLTimeTag.Size):
                            sz = nd_string_par.Attributes.GetNamedItem(
                                XMLDefines.XMLTimeAttr.SizeValue).Value;
                            break;

                        case (XMLDefines.XMLTimeTag.Data):
                            dt_str = nd_string_par.Attributes.GetNamedItem(
                                XMLDefines.XMLTimeAttr.DataTmFlag).Value;
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
                out_pageEl = new PageTime(b_pos_x, b_pos_y, color, b_sz);

                ((PageTime)out_pageEl).Second = (dt_str[0] == '1');
                ((PageTime)out_pageEl).Minute = (dt_str[1] == '1');
                ((PageTime)out_pageEl).Hour = (dt_str[2] == '1');
            }
            catch (Exception e)
            {
            }
            return out_pageEl;
        }
    }
}
