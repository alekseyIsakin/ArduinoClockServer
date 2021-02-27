using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Xml;

using ArdClock.src;
using ArdClock.src.UIGenerate;
using ArdClock.src.ArdPage;
using ArdClock.src.ArdPage.HelpingClass;
using ArdClock.src.ArdPage.PageElements;

namespace ArdClock.src.XMLLoader
{
    static class Writer
    {
        static public void WritePageListToXML(List<APage> pageList, string fileName)
        {
            XmlDocument xdd = new XmlDocument();

            var xmlDeclaration = xdd.CreateXmlDeclaration("1.0", "UTF-8", null);
            var root = xdd.CreateElement(
                (XMLDefines.XMLTag.Pages).ToString());

            xdd.AppendChild(xmlDeclaration);
            xdd.AppendChild(root);

            if (pageList.Count > 0)
            {
                foreach (var page in pageList)
                {
                    // Создаём нод для описания страницы
                    var xmlPage = xdd.CreateElement(
                        (XMLDefines.XMLTag.Page).ToString());

                    // Аттрибут для описания имени и ID
                    var attrName =
                        xdd.CreateAttribute(XMLDefines.XMLPageAttr.Name.ToString());
                    var attrID =
                        xdd.CreateAttribute(XMLDefines.XMLPageAttr.ID.ToString());

                    attrName.Value = page.Name;
                    attrID.Value = page.ID.ToString();

                    xmlPage.Attributes.Append(attrName);
                    xmlPage.Attributes.Append(attrID);

                    if (page.Elements.Count > 0)
                    {
                        foreach (var pageEl in page.Elements)
                        {
                            var xmlEl = PageElCenter.TryWriteToXml(pageEl, xdd);

                            if (xmlEl != null)
                                xmlPage.AppendChild(xmlEl);
                        }
                    }

                    root.AppendChild(xmlPage);
                }
            }

            try
            { 
                xdd.Save(fileName);
                
            }
            catch
            { }
        }
    }
}
