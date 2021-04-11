using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BaseLib.DateTimeChecker
{
    public class DateTimeChecker
    {
        private DateTime LastSendDate = new DateTime();
        private TimeSpan Offset = new TimeSpan();
        private TimeSpan Period = new TimeSpan();
        
        public void SetOffset(TimeSpan ts) { Offset = ts; }
        public void SetPeriod(TimeSpan ts) { Period = ts; }
        public void SetPeriod(DateTime dt) { LastSendDate = dt; }

        public bool CheckTimeSend() 
        {
            TimeSpan timePass = _update();

            if (timePass > Period)
                return false;

            return true;
        }

        private TimeSpan _update() 
        {
            TimeSpan timePass = DateTime.Now - LastSendDate;
            if (timePass > Offset)
            {
                LastSendDate = DateTime.Now;
            }
            return timePass;
        }
    }



}
