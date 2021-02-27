using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;

using ArdClock.src.HelpingClass;
using ArdClock.src.ArdPage;
using ArdClock.src.ArdPage.PageElements;

namespace ArdClock.src.SerialControl
{
    class DataSender
    {
        public event EventHandler TimerIsOver;
        public event EventHandler SuccSend;

        const int TIME_WAIT = 2; // время на обработку отправки
        
        System.Windows.Threading.DispatcherTimer timer;

        bool ReadyToSend = false;

        public int BaudRate{ get; private set; }
        public string PortName{ get; private set; }
        private SerialPort SPort;

        public DataSender() : this("None", 300) { }
 
        public DataSender(string portName, int baudRate) 
        {
            timer = new System.Windows.Threading.DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(TIME_WAIT);
            timer.Tick += TimerElapsed;
            SPort = new SerialPort();

            this.PortName = portName;
            this.BaudRate = baudRate;

            try
                { SPort = new SerialPort(this.PortName, this.BaudRate); }
            catch
                { throw; }

            SPort.Parity = Parity.None;
            SPort.StopBits = StopBits.One;
            SPort.DataBits = 8;
            SPort.Handshake = Handshake.None;
            SPort.RtsEnable = true;
            
            ReadyToSend = true;
        }
        public bool IsConnect() { return SPort.IsOpen; }

        public void SetPortName(string PortName) { SPort.PortName = PortName; }
        public void SetBaudRate(int BaudRate) { SPort.BaudRate = BaudRate; }

        public void Connect() { SPort.Open(); }
        public void Disconnect() { SPort.Close(); }

        public void Send(src.ArdPage.APage page)
        {
            List<byte> arrOut = new List<byte>();
            arrOut = page.GenSendData();

            if (arrOut.Count == 0)
                return;

            TrySend(arrOut);
        }
        public void SendClearCode() 
        {
            TrySend((new PageClear()).GenSendData());
        }

        private void TrySend(List<byte> byteArr) 
        {
            if (ReadyToSend)
            {
                try
                {
                    SPort.Write(byteArr.ToArray(), 0, byteArr.Count);
                    ReadyToSend = false;

                    if (SuccSend != null)
                        SuccSend(this, null);

                    timer.Start();
                }
                catch
                {
                    throw;
                }
            }
        }

        private void TimerElapsed(object sender, EventArgs e) 
        {
            ReadyToSend = true;

            if (TimerIsOver != null)
                TimerIsOver(this, null);

            timer.Stop();
        }
    }
}
