﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using BaseLib;

namespace ArdClock.ArdPage
{
    public static partial class PageElCenter
    {
        private static readonly Dictionary<int, BaseConstruct> _funcsConstruct = new Dictionary<int, BaseConstruct>();
        private static readonly Dictionary<int, BaseUIConstruct> _funcsUIConstruct = new Dictionary<int, BaseUIConstruct>();

        static private readonly List<int> _index = new List<int>();
        static private readonly Dictionary<int, string> _namesPageEl = new Dictionary<int,string>();

        static PageElCenter()
        {
            _index.Add(PageClear.ID);

            foreach (var LibEL in Lib.ExternalBaseLib.GetExternalLibs()) 
            { AddNewElement(LibEL); }
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

        public static AbstrPageEl GetNewPageElFromID(int id)
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

        public static Dictionary<int,string> GetDict()
        { return _namesPageEl; }

        public static void AddNewElement(ExternalLib externalLib)
        {
            if (!HasID(externalLib.ID))
            {

                _index.Add(externalLib.ID);

                _namesPageEl.Add(externalLib.ID,
                                 externalLib.Name);

                _funcsConstruct.Add  (externalLib.ID, externalLib.Construct);
                _funcsUIConstruct.Add(externalLib.ID, externalLib.UIConstruct);
            }
        }
    }
}
