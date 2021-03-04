using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using BaseLib;
using Lib.String;
using Lib.Time;


namespace Lib
{
    static public class ExternalBaseLib
    {
        static public List<ExternalLib> GetExternalLibs() 
        {
            List<ExternalLib> externalLibs = new List<ExternalLib>();

            ExternalLib stringDt = new ExternalLib(PageString.ID, PageString.Name,
                () => new PageString(),
                (pl) => new UIPageString(pl),
                StringXmlBehavior.ReadXMLPageString,
                StringXmlBehavior.WritePageStringToXml);

            ExternalLib timeDt = new ExternalLib(PageTime.ID, PageTime.Name,
                () => new PageTime(),
                (pl) => new UIPageTime(pl),
                TimeXmlBehavior.ReadXMLPageTime,
                TimeXmlBehavior.WritePageTimeToXml);

            externalLibs.Add(stringDt);
            externalLibs.Add(timeDt);


            return externalLibs;
        }
    }
}
