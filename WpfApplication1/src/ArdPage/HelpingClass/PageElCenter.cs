using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ArdClock.src.ArdPage;
using ArdClock.src.UIGenerate;
using ArdClock.src.ArdPage.PageElements;

using BaseLib;

namespace ArdClock.src.ArdPage.HelpingClass
{
    public static partial class PageElCenter
    {
        private static Func<AbstrPageEl>[] _funcsConstruct = new Func<AbstrPageEl>[128];

        private static Func<System.Xml.XmlNode, AbstrPageEl>[] _funcsXmlLoaders
            = new Func<System.Xml.XmlNode, AbstrPageEl>[128];
        private static Func<AbstrPageEl, System.Xml.XmlDocument, System.Xml.XmlElement>[] _funcsXmlWriters
            = new Func<AbstrPageEl, System.Xml.XmlDocument, System.Xml.XmlElement>[128];

        public static Func<AbstrPageEl, AbstrUIBase>[] _funcsUIConstruct
            = new Func<AbstrPageEl, AbstrUIBase>[128];

        static private List<int> _index = new List<int>();
        static private Dictionary<int, string> _namesPageEl = new Dictionary<int,string>();

        const int TBaseEl = 0;
        const int TString = 65;
        const int TTime = 66;
        const int TClearCode = 127;

        static PageElCenter()
        {
            _index.Add(TBaseEl);
            _index.Add(TString);
            _index.Add(TTime);
            _index.Add(TClearCode);

            _namesPageEl.Add(TString, "String");
            _namesPageEl.Add(TTime, "Time");

            _funcsConstruct[TBaseEl] = () => new PageEl();
            _funcsConstruct[TString] = () => new PageString();
            _funcsConstruct[TTime] = () => new PageTime();

            _funcsXmlLoaders[TString] = (nd) => ReadLikePageString(nd);
            _funcsXmlLoaders[TTime] = (nd) => ReadLikePageTime(nd);

            _funcsXmlWriters[TString] = (ps, xd) => XmlElFromPageString(ps, xd);
            _funcsXmlWriters[TTime] = (ps, xd) => XmlElFromPageTime(ps, xd);

            _funcsUIConstruct[TString] = (pEl) => new UIPageString(pEl);
            _funcsUIConstruct[TTime] = (pEl) => new UIPageTime(pEl);
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

        public static void AddNewElement(int id)
        {
            if (!HasID(id))
            {

            }
        }
    }
}
