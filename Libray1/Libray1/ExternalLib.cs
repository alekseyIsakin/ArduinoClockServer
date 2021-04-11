using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BaseLib
{
    public delegate AbstrPageEl BaseConstruct();
    public delegate AbstrUIBase BaseUIConstruct(AbstrPageEl abstrUIBase);

    public sealed class ExternalLib
    {
        public readonly int ID;
        public readonly string Name;        
        public readonly BaseConstruct Construct;
        public readonly BaseUIConstruct UIConstruct;
        
        public ExternalLib(int id, string name, 
            BaseConstruct construct,
            BaseUIConstruct uiConstruct)
           
        {
            this.ID = id;
            this.Name = name;
            this.Construct = construct;
            this.UIConstruct = uiConstruct;
        }
    }
}
