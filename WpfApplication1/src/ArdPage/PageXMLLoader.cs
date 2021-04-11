﻿using System;
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
                System.IO.StreamReader sw = new System.IO.StreamReader(fileName);
                pageList = (List<APage>)serializer.Deserialize(sw);
                sw.Close();
            }
            catch (System.IO.FileNotFoundException e)
            {
                MessageBox.Show("В директории не неайден существующий файл с настройками, поэтому будет создан новый\n" + e.Source);
                pageList.Add(new APage());
                WritePageListToXML(pageList, fileName); 
            }

            return pageList;
        }
        static public List<APage> OldLoadPageListFromXML(string fileName)
        {
            
            XmlDocument xdd = new XmlDocument();
            List<APage> pageList = new List<APage>();

            try
            {
                xdd.Load(fileName);

                XmlNode root = xdd.DocumentElement;

                if (root.HasChildNodes)
                {
                    foreach (XmlNode nd_page in root)
                    {
                        // Просмотр записанных страниц
                        // Чтение имени и ID страницы
                        XmlNode ndName = nd_page.Attributes.GetNamedItem(XMLDefines.XMLPageAttr.Name);
                        XmlNode ndID = nd_page.Attributes.GetNamedItem(XMLDefines.XMLPageAttr.ID);

                        List<AbstrPageEl> page_elements = new List<AbstrPageEl>();
                        foreach (XmlNode nd_el in nd_page)
                        {
                            if (nd_el.Name == XMLDefines.XMLTag.PageEl)
                            {
                                // Собираем элемент страницы по шаблону
                                //

                                int type_ep = int.Parse(
                                    nd_el.Attributes.GetNamedItem(
                                        XMLDefines.XMLBaseElAttr.TypeEl
                                    ).Value);

                                PageEl pl = (PageEl)TryLoadFromXml(type_ep, nd_el);

                                if (pl != null)
                                { page_elements.Add(pl); }
                            }
                        }

                        APage page = new APage(
                            ndName.Value,
                            int.Parse(ndID.Value),
                            page_elements
                            );
                        pageList.Add(page);
                    }
                }
            }
            catch (System.IO.FileNotFoundException ex)
            {
                MessageBox.Show("В директории не неайден существующий файл с настройками, поэтому будет создан новый.");
                pageList.Add(new APage());
                WritePageListToXML(pageList, fileName);
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }

            if (pageList.Count == 0) { pageList.Add(new APage()); }

            return pageList;
        }
        
    }
}
