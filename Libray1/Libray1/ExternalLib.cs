using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BaseLib
{
    public delegate AbstrPageEl BaseConstruct();
    public delegate AbstrUIBase BaseUIConstruct(AbstrPageEl abstrUIBase);
    public delegate AbstrPageEl BaseXMLLoader(System.Xml.XmlNode xmlNode);
    public delegate System.Xml.XmlElement BaseXMLWriter(AbstrPageEl abstrPageEl, System.Xml.XmlDocument xmlDocument);

    public sealed class ExternalLib
    {
        public readonly int ID;
        public readonly string Name;        
        public readonly BaseConstruct Construct;
        public readonly BaseUIConstruct UIConstruct;
        public readonly BaseXMLLoader XMLLoader;
        public readonly BaseXMLWriter XMLWriter;
        
        public ExternalLib(int id, string name, 
            BaseConstruct construct,
            BaseUIConstruct uiConstruct,
            BaseXMLLoader xmlLoader,
            BaseXMLWriter xmlWriter)
           
        {
            this.ID = id;
            this.Name = name;
            this.Construct = construct;
            this.UIConstruct = uiConstruct;
            this.XMLLoader = xmlLoader;
            this.XMLWriter = xmlWriter;
        }
    }
}
