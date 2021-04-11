using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using BaseLib;
using Lib.String;
using Lib.Time;


namespace Lib
{
    enum Elements 
    {
        STRING = 65,
        TIME = 66,
        MAIL
    }
    static public class ExternalBaseLib
    {
        static public List<ExternalLib> GetExternalLibs() 
        {
            List<ExternalLib> externalLibs = new List<ExternalLib>();

            ExternalLib stringDt = new ExternalLib(PageString.ID, PageString.Name,
                () => new PageString(),
                (pl) => new UIPageString(pl));

            ExternalLib timeDt = new ExternalLib(PageTime.ID, PageTime.Name,
                () => new PageTime(),
                (pl) => new UIPageTime(pl));

            externalLibs.Add(stringDt);
            externalLibs.Add(timeDt);


            return externalLibs;
        }
    }
}
