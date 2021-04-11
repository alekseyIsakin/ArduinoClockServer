using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BaseLib
{
    [System.Xml.Serialization.XmlInclude(typeof(Lib.Time.PageTime))]
    [System.Xml.Serialization.XmlInclude(typeof(Lib.String.PageString))]
    public abstract class AbstrPageEl
    {
        public static readonly byte ID;
        public const string Name = "AbstrPageEl";
        
        #region Serialize
        [System.Xml.Serialization.XmlAttribute]
        public string CustomName; 
        #endregion
        public abstract List<byte> GenSendData();
        public abstract byte GetTypeEl();
        public abstract string GetNameEl();
    }
}
