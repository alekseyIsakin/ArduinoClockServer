using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

using BaseLib;
using BaseLib.Xml;

namespace Lib.Time
{
    public static partial class TimeXmlBehavior
    {
        static public XmlElement WritePageTimeToXml(AbstrPageEl pEl, XmlDocument xdd)
        {
            PageTime pt = (PageTime)pEl;
            // Описание элемента
            var ndPageEl = xdd.CreateElement(
                (XMLDefines.XMLTag.PageEl).ToString());

            var attrTypeEl = xdd.CreateAttribute(
                XMLDefines.XMLBaseElAttr.TypeEl.ToString());

            attrTypeEl.Value = ((int)pt.GetTypeEl()).ToString();

            ndPageEl.Attributes.Append(attrTypeEl);

            // Позиция
            var ndPos = xdd.CreateElement(
                XMLDefines.XMLBaseElTag.Position.ToString());

            var attrPosX = xdd.CreateAttribute(
                XMLDefines.XMLBaseElAttr.PosX.ToString());
            var attrPosY = xdd.CreateAttribute(
                XMLDefines.XMLBaseElAttr.PosY.ToString());

            attrPosX.Value = pt.X.ToString();
            attrPosY.Value = pt.Y.ToString();

            ndPos.Attributes.Append(attrPosX);
            ndPos.Attributes.Append(attrPosY);

            // Цвет
            var ndClr = xdd.CreateElement(
                XMLDefines.XMLBaseElTag.Color.ToString());

            var attrClr = xdd.CreateAttribute(
                XMLDefines.XMLBaseElAttr.ColorValue.ToString());

            attrClr.Value = pt.TextColor.ToHex();

            ndClr.Attributes.Append(attrClr);

            // Размер
            var ndSz = xdd.CreateElement(
                XMLDefines.XMLBaseElTag.Size.ToString());

            var attrSz = xdd.CreateAttribute(
                XMLDefines.XMLBaseElAttr.SizeValue.ToString());

            attrSz.Value = pt.Size.ToString();

            ndSz.Attributes.Append(attrSz);

            // Данные
            var ndDt = xdd.CreateElement(
                XMLDefines.XMLBaseElTag.Data.ToString());

            var attrSec = xdd.CreateAttribute(
                XMLDefines.XMLBaseElAttr.Data.ToString());

            attrSec.Value = pt.Second ? "1" : "0";
            attrSec.Value += pt.Minute ? "1" : "0";
            attrSec.Value += pt.Hour ? "1" : "0";

            ndDt.Attributes.Append(attrSec);

            // CustomName
            var ndCustNm = xdd.CreateElement(
                XMLDefines.XMLBaseElTag.CustomName.ToString());

            var attrCustNm = xdd.CreateAttribute(
                XMLDefines.XMLBaseElAttr.Data.ToString());

            attrCustNm.Value = pt.CustomName;
            ndCustNm.Attributes.Append(attrCustNm);

            //
            ndPageEl.AppendChild(ndPos);
            ndPageEl.AppendChild(ndClr);
            ndPageEl.AppendChild(ndSz);
            ndPageEl.AppendChild(ndDt);
            ndPageEl.AppendChild(ndCustNm);

            return ndPageEl;
        }
    }
}
