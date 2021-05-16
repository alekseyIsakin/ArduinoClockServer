using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;

using Lib;

namespace ArdClock.SerialControl
{
    class DataSender
    {
        public event EventHandler AvailSend;
        public event EventHandler SuccSend;

        const int TIME_WAIT = 2; // время на обработку отправки
        const int MAX_MSG_LEN = 64; // максимальное количество отправяемых символов

        System.Windows.Threading.DispatcherTimer timer;

        public bool ReadyToSend { get; private set; } = false;

        public int BaudRate{ get; private set; }
        public string PortName{ get; private set; }
        private readonly SerialPort SPort;

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
        public void Disconnect() { SPort.Close(); ReadyToSend = false; }

        public void Send(ArdPage.APage page)
        {
            List<byte> arrOut = page.GenSendData();

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
                try
                {
                    // Добавить предупреждение о переполнении
                    SPort.Write(byteArr.ToArray(), 0, byteArr.Count);
                    ReadyToSend = false;

                    SuccSend?.Invoke(this, null);

                    timer.Start();
                }
                catch
                { throw; }
        }

        private void TimerElapsed(object sender, EventArgs e) 
        {
            ReadyToSend = true;

            AvailSend?.Invoke(this, null);

            timer.Stop();
        }
    }
}
