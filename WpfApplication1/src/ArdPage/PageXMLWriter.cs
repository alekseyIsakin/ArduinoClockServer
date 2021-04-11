using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Xml;
using System.Xml.Serialization;

using BaseLib.Xml;


namespace ArdClock.ArdPage
{
    public static partial class PageElCenter
    {
        static public void WritePageListToXML(List<APage> pageList, string fileName)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<APage>));
            System.IO.StreamWriter sw = new System.IO.StreamWriter(fileName);
            try
            {
                serializer.Serialize(sw, pageList);
                sw.Close();
            }
            catch 
            { }
        }
        static public void OldWritePageListToXML(List<APage> pageList, string fileName)
        {

            XmlDocument xdd = new XmlDocument();

            System.IO.StreamWriter sw = new System.IO.StreamWriter("test.xml");
            XmlSerializer serializer = new XmlSerializer(typeof(List<APage>));
            sw.WriteLine("Hello World!!");
            serializer.Serialize(sw, pageList);
            sw.Close();


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
                            var xmlEl = TryWriteToXml(pageEl, xdd);

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
