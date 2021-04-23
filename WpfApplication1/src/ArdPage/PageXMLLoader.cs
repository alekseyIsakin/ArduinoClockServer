using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Xml;
using System.Xml.Serialization;

using BaseLib;
using BaseLib.Xml;

namespace ArdClock.ArdPage
{
    public static partial class PageElCenter
    {
        static public List<APage> LoadPageListFromXML(string fileName)
        {
            List<APage> pageList = new List<APage>();

            XmlSerializer serializer = new XmlSerializer(typeof(List<APage>));

            try
            {
                using (var sw = new System.IO.StreamReader(fileName))
                {
                    pageList = (List<APage>)serializer.Deserialize(sw);
                }
            }
            catch (System.IO.FileNotFoundException e)
            {
                MessageBox.Show("В директории не неайден существующий файл с настройками, поэтому будет создан новый\n" + e.Source);
                pageList.Add(new APage());
                WritePageListToXML(pageList, fileName);
            }

            return pageList;
        }
    }
}
