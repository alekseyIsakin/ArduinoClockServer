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
        public static XmlElement TryWriteToXml(AbstrPageEl pl, XmlDocument xdd)
        {
            byte tp = pl.GetTypeEl();

            if (HasID(tp)) 
                return _funcsXmlWriters[tp](pl, xdd);
            return null;
        }

        static private XmlElement XmlElFromPageString(AbstrPageEl pEl, XmlDocument xdd)
        {
            PageString ps = (PageString)pEl; 

            // Описание элемента
            var ndPageEl = xdd.CreateElement(
                (XMLDefines.XMLTag.PageEl).ToString());

            var attrTypeEl = xdd.CreateAttribute(
                XMLDefines.XMLStringAttr.TypeEl.ToString());

            attrTypeEl.Value = ((int)ps.GetTypeEl()).ToString();

            ndPageEl.Attributes.Append(attrTypeEl);

            // Позиция
            var ndPos = xdd.CreateElement(
                XMLDefines.XMLStringTag.Position.ToString());

            var attrPosX = xdd.CreateAttribute(
                XMLDefines.XMLStringAttr.PosX.ToString());
            var attrPosY = xdd.CreateAttribute(
                XMLDefines.XMLStringAttr.PosY.ToString());

            attrPosX.Value = ps.X.ToString();
            attrPosY.Value = ps.Y.ToString();

            ndPos.Attributes.Append(attrPosX);
            ndPos.Attributes.Append(attrPosY);

            // Цвет
            var ndClr = xdd.CreateElement(
                XMLDefines.XMLStringTag.Color.ToString());

            var attrClr = xdd.CreateAttribute(
                XMLDefines.XMLStringAttr.ColorValue.ToString());

            attrClr.Value = ps.TextColor.ToHex();

            ndClr.Attributes.Append(attrClr);

            // Размер
            var ndSz = xdd.CreateElement(
                XMLDefines.XMLStringTag.Size.ToString());

            var attrSz = xdd.CreateAttribute(
                XMLDefines.XMLStringAttr.SizeValue.ToString());

            attrSz.Value = ps.Size.ToString();

            ndSz.Attributes.Append(attrSz);

            // Текст
            var ndDt = xdd.CreateElement(
                XMLDefines.XMLStringTag.Data.ToString());

            var attrDt = xdd.CreateAttribute(
                XMLDefines.XMLStringAttr.Data.ToString());

            attrDt.Value = ps.Data;

            ndDt.Attributes.Append(attrDt);


            //
            ndPageEl.AppendChild(ndPos);
            ndPageEl.AppendChild(ndClr);
            ndPageEl.AppendChild(ndSz);
            ndPageEl.AppendChild(ndDt);

            return ndPageEl;
        }
        static private XmlElement XmlElFromPageTime(AbstrPageEl pEl, XmlDocument xdd)
        {
            PageTime pt = (PageTime)pEl;
            // Описание элемента
            var ndPageEl = xdd.CreateElement(
                (XMLDefines.XMLTag.PageEl).ToString());

            var attrTypeEl = xdd.CreateAttribute(
                XMLDefines.XMLTimeAttr.TypeEl.ToString());

            attrTypeEl.Value = ((int)pt.GetTypeEl()).ToString();

            ndPageEl.Attributes.Append(attrTypeEl);

            // Позиция
            var ndPos = xdd.CreateElement(
                XMLDefines.XMLTimeTag.Position.ToString());

            var attrPosX = xdd.CreateAttribute(
                XMLDefines.XMLTimeAttr.PosX.ToString());
            var attrPosY = xdd.CreateAttribute(
                XMLDefines.XMLTimeAttr.PosY.ToString());

            attrPosX.Value = pt.X.ToString();
            attrPosY.Value = pt.Y.ToString();

            ndPos.Attributes.Append(attrPosX);
            ndPos.Attributes.Append(attrPosY);

            // Цвет
            var ndClr = xdd.CreateElement(
                XMLDefines.XMLTimeTag.Color.ToString());

            var attrClr = xdd.CreateAttribute(
                XMLDefines.XMLTimeAttr.ColorValue.ToString());

            attrClr.Value = pt.TextColor.ToHex();

            ndClr.Attributes.Append(attrClr);

            // Размер
            var ndSz = xdd.CreateElement(
                XMLDefines.XMLTimeTag.Size.ToString());

            var attrSz = xdd.CreateAttribute(
                XMLDefines.XMLTimeAttr.SizeValue.ToString());

            attrSz.Value = pt.Size.ToString();

            ndSz.Attributes.Append(attrSz);

            // Данные
            var ndDt = xdd.CreateElement(
                XMLDefines.XMLTimeTag.Data.ToString());

            var attrSec = xdd.CreateAttribute(
                XMLDefines.XMLTimeAttr.DataTmFlag.ToString());

            attrSec.Value = pt.Second ? "1" : "0";
            attrSec.Value += pt.Minute ? "1" : "0";
            attrSec.Value += pt.Hour ? "1" : "0";

            ndDt.Attributes.Append(attrSec);


            //
            ndPageEl.AppendChild(ndPos);
            ndPageEl.AppendChild(ndClr);
            ndPageEl.AppendChild(ndSz);
            ndPageEl.AppendChild(ndDt);

            return ndPageEl;
        }
    }
}
