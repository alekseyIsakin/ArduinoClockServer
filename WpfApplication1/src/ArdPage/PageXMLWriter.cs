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

            try
            {
                using (var sw = new System.IO.StreamWriter(fileName))
                {
                    serializer.Serialize(sw, pageList);
                }
            }
            catch (Exception e)
            { MessageBox.Show("Не удалось сохранить файл настроек" + e.Message); }
        }
    }
}
