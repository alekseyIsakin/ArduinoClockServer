﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Lib;

namespace ArdClock.ArdPage
{
    /*
     Объявление всех классов, которые будут хранить
     представление данных, которые будут отрисованы
     на ардуино
    */

    //
    // Хранит имя и ID одной страницы
    // а также сведения об элементах на данной странице
    //

    public class APage
    {
        [System.Xml.Serialization.XmlAttribute]
        public string Name;
        [System.Xml.Serialization.XmlAttribute]
        public int ID;
        public List<AbstrPageEl> Elements { get; private set; }

        public APage(string name, int id, List<AbstrPageEl> elements) 
        {
            Name = name;
            ID = id;
            Elements = elements;
        }

        public APage() :
            this("EmptyPage", -1, new List<AbstrPageEl>()) { }

        public bool TestPage(List<Byte> in_out_dt=null) 
        {
            bool result = true;
            List<Byte> out_dt = new List<byte>();

            if (in_out_dt == null)
            {
                foreach (var e in Elements)
                {
                    out_dt.AddRange(e.GenSendData());
                }
            }
            else
                out_dt = in_out_dt;

            if (out_dt.Count > 64)
                result = false;

            return result;
        }
        
        public override string ToString() 
        { return string.Format("Page {0}", Name); }

        public List<byte> GenSendData()
        {
            bool overflow = false;

            List<byte> out_dt = new List<byte>();

            if (Elements == null)
            {
                System.Windows.MessageBox.Show("Пустая страница");
                return out_dt;
            }
            if (out_dt.Count <=64 )
                foreach (var e in Elements)
                {
                    List<Byte> lb_out = e.GenSendData();
                    if (lb_out.Count + out_dt.Count <= 64)
                        out_dt.AddRange(lb_out);
                    else
                        overflow = true;
                }
            if (overflow)
                System.Windows.MessageBox.Show("Слишком много данных, некоторые элементы могли не передаться на устройство");

            return out_dt;
        }
    }
}
