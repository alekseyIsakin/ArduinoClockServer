using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using BaseLib;

namespace ArdClock.ArdPage
{
    public static partial class PageElCenter
    {
        private static BaseConstruct[] _funcsConstruct = new BaseConstruct[128];
        private static BaseXMLLoader[] _funcsXmlLoaders = new BaseXMLLoader[128];
        private static BaseXMLWriter[] _funcsXmlWriters = new BaseXMLWriter[128];
        private static BaseUIConstruct[] _funcsUIConstruct = new BaseUIConstruct[128];

        static private List<int> _index = new List<int>();
        static private Dictionary<int, string> _namesPageEl = new Dictionary<int,string>();

        const int TBaseEl = 0;
        const int TClearCode = 127;

        static PageElCenter()
        {
            _index.Add(PageEl.ID);
            _index.Add(PageClear.ID);

            AddNewElement(Lib.ExternalBaseLib.GetExternalLibs()[0]);
            AddNewElement(Lib.ExternalBaseLib.GetExternalLibs()[1]);
        }
        public static AbstrUIBase TryGenUiControl(int id)
        {
            if (HasID(id))
                return _funcsUIConstruct[id](
                    _funcsConstruct[id]());
            return null;
        }
        public static AbstrUIBase TryGenUiControl(AbstrPageEl pEl) 
        {
            int id = pEl.GetTypeEl();
            if (HasID(id))
                return _funcsUIConstruct[id](pEl);
            return null;
        }

        public static AbstrPageEl getNewPageElFromID(int id)
        {
            if (HasID(id))
                return _funcsConstruct[id]();
            return null;
        }

        public static bool HasID(int id)
        {
            foreach (var i in _index)
                if (i == id)
                    return true;
            return false;
        }

        public static Dictionary<int,string> getDict()
        { return _namesPageEl; }

        public static void AddNewElement(ExternalLib externalLib)
        {
            if (!HasID(externalLib.ID))
            {

                _index.Add(externalLib.ID);

                _namesPageEl.Add(externalLib.ID,
                                 externalLib.Name);

                _funcsConstruct[externalLib.ID]     = externalLib.Construct;
                _funcsXmlLoaders[externalLib.ID]    = externalLib.XMLLoader;
                _funcsXmlWriters[externalLib.ID]    = externalLib.XMLWriter;
                _funcsUIConstruct[externalLib.ID]   = externalLib.UIConstruct;
            }
        }
        public static System.Xml.XmlElement TryWriteToXml(AbstrPageEl pl, System.Xml.XmlDocument xdd)
        {
            byte tp = pl.GetTypeEl();

            if (HasID(tp))
                return _funcsXmlWriters[tp](pl, xdd);
            return null;
        }
        public static AbstrPageEl TryLoadFromXml(int id, System.Xml.XmlNode nd)
        {
            if (HasID(id))
                return _funcsXmlLoaders[id](nd);
            return null;
        }
    }
}
