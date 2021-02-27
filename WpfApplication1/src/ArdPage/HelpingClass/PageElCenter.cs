using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ArdClock.ArdPage;
using ArdClock.UIGenerate;
using ArdClock.ArdPage.PageElements;

using BaseLib;

namespace ArdClock.ArdPage.HelpingClass
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
        const int TString = 65;
        const int TTime = 66;
        const int TClearCode = 127;

        static PageElCenter()
        {
            _index.Add(TBaseEl);
            _index.Add(TClearCode);

            ExternalLib elString = new ExternalLib(TString, "String",
                () => new PageString(),
                (pEl) => new UIPageString(pEl),
                (nd) => ReadLikePageString(nd),
                (ps, xd) => XmlElFromPageString(ps, xd));

            ExternalLib elTime = new ExternalLib(TTime, "Time",
                () => new PageTime(),
                (pEl) => new UIPageTime(pEl),
                (nd) => ReadLikePageTime(nd),
                (ps, xd) => XmlElFromPageTime(ps, xd));

            AddNewElement(elString);
            AddNewElement(elTime);
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
    }
}
