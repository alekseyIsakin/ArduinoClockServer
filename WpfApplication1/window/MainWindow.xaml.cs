using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO.Ports;

using ArdClock.src.HelpingClass;
using ArdClock.src.ArdPage.PageElements;
using ArdClock.src.ArdPage;

namespace ArdClock
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //private System.Timers.Timer timer2;
        private System.Windows.Threading.DispatcherTimer timer;

        private src.SerialControl.DataSender DSender;
        private src.ArdPage.APage SenderPage;

        public window.PageEditorWindow PEWindow;

        //
        // Предотвращение запуска второго экземпляра программы
        //
        private static System.Threading.Mutex mt;
        private static bool isSingleProgram;

        static MainWindow()
        {
            mt = new System.Threading.Mutex(false, " ", out isSingleProgram);

            if (!isSingleProgram)
                System.Diagnostics.Process.GetCurrentProcess().Kill();
        }
        //
        //
        //

        //
        // Логика NotifyIcon
        //
        private src.NIcon NIcon = null;

        public void notifyIcon_Click(object sender, EventArgs e)
        {
            if (((System.Windows.Forms.MouseEventArgs)e).Button == System.Windows.Forms.MouseButtons.Left)
            {
                if (Visibility == Visibility.Hidden)
                {
                    Visibility = Visibility.Visible;
                }
                Activate();
            }
        }
        private void Window_StateChanged(object sender, EventArgs e)
        {
            if (WindowState == WindowState.Minimized) 
            {
                ChangeVisbleWindow();
                WindowState = WindowState.Normal;
            }
        }
        public void onClose(object sender, EventArgs e) { Application.Current.Shutdown(); }
        public void ChangeVisbleWindow() 
        {
            Visibility = (Visibility == Visibility.Hidden) ? Visibility.Visible : 
                                                             Visibility.Hidden;

        }


        //
        //
        //
        public MainWindow()
        {
            InitializeComponent();

            NIcon = new src.NIcon(Icon);
            NIcon.Click += notifyIcon_Click;
            NIcon.DoubleClick += notifyIcon_Click;

            NIcon.ContextMenuClose += onClose;
            NIcon.ContextMenuConnect += ConnectPortContext_Click;

            timer = new System.Windows.Threading.DispatcherTimer();
            timer.Tick += TimerElapsed;

            DSender = new src.SerialControl.DataSender();
            DSender.TimerIsOver += EnableSend;
            DSender.SuccSend += LockSend;
            string[] lstSpd = { "300", "1200", "2400", "4800", "9600", "19200", "38400" };
            comboBoxSPD.ItemsSource = lstSpd;
            comboBoxSPD.SelectedIndex = 4;

            if (SerialPort.GetPortNames().Length > 0)
            {
                comboBoxPort.ItemsSource = SerialPort.GetPortNames();
                comboBoxPort.SelectedIndex = 0;
            }
            else
            {
                MessageBox.Show("Нет доступных портов");
            }

            PEWindow = new window.PageEditorWindow();

            if (PEWindow.pageList.Count >= 1)
                SenderPage = PEWindow.pageList[0];
            else
                SenderPage = new APage();
            PEWindow.Close();
        }

        private void ConnectPort_Click(object sender, RoutedEventArgs e)
        { Connect(); }
        private void ConnectPortContext_Click(object sender, EventArgs e) 
        { Connect(); }

        private void Connect() 
        {
            if (DSender.IsConnect())
            { 
                DSender.Disconnect();
                StopTimer();
            }
            else
            {
                try
                {
                    string portName = comboBoxPort.Text;
                    int baudRate = int.Parse(comboBoxSPD.Text);

                    if (DSender.BaudRate != baudRate || DSender.PortName != portName)
                    {
                        DSender.SetBaudRate(baudRate);
                        DSender.SetPortName(portName);
                    }
                    DSender.Connect();

                    if ((bool)TimerCheckBox.IsChecked)
                    {
                        timer.Start();
                        TimerElapsed(timer, null);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка: " + ex.Message);
                }
            }

            try
            { setConnectGuiState(DSender.IsConnect()); }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }
        private void setConnectGuiState(bool state)
        {
            try
            {
                if (state)
                {
                    string portName = comboBoxPort.Text;
                    int baudRate = int.Parse(comboBoxSPD.Text);

                    CurPortLabel.Content = portName + " " + baudRate.ToString();
                    ConnectPortButton.Content = "Отключиться";
                }
                else
                {
                    CurPortLabel.Content = "Disconnect";
                    ConnectPortButton.Content = "Подключиться";
                }
                NIcon.SetIcon(DSender.IsConnect());
                LockEditPortField(!state);
            }
            catch { throw; }
        }

        private void LockEditPortField(bool state) 
        {
            comboBoxSPD.IsEnabled    = state;
            comboBoxPort.IsEnabled   = state;
            //TimerCheckBox.IsEnabled = !state;
        }

        // Запуск/Остановка таймера
        private void TimerCheckBox_Click(object sender, RoutedEventArgs e)
        {
            if (DSender.IsConnect())
            {
                timer.Interval = TimeSpan.FromSeconds(
                    Convert.ToInt32(((ComboBoxItem)TimeCountComboBox.SelectedItem).Content));

                if ((bool)TimerCheckBox.IsChecked)
                {
                    SendCurPage();
                    timer.Start();
                }
                else
                    StopTimer();
            }
        }

        private void TimerElapsed(object sender, EventArgs e) 
        {
            SendCurPage();

            if ((bool)TimerCheckBox.IsChecked) 
            { timer.Start(); }
        }

        private void StopTimer() 
        {
            TimerCheckBox.IsChecked = false;
            timer.Stop();
        }

        //

        private void SendCurPage() 
        {
            try
            {
                DSender.Send(SenderPage);
                Title = System.DateTime.Now.ToLongTimeString();
            }
            catch (Exception ex)
            {
                StopTimer();
                DSender.Disconnect();
                setConnectGuiState(DSender.IsConnect());
                MessageBox.Show("Ошибка отправки: " + ex.Message);
            }
            
        }


        // events
        //
        private void PageSettingButton_Click(object sender, RoutedEventArgs e)
        {
            PEWindow = new window.PageEditorWindow();
            PEWindow.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            PEWindow.button_Activate.Click += ButtonActivate_Click;
            PEWindow.ShowDialog();
        }

        private void MenuAboutAppClick(object sender, EventArgs e) 
        {
            MessageBox.Show("Версия сборки: " + 
                System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString());
        }

        private void LockSend(object sender, EventArgs e)
        { Button_singleSend.IsEnabled = false; }

        private void EnableSend(object sender, EventArgs e)
        { Button_singleSend.IsEnabled = true; }

        private void ButtonActivate_Click(object sender, RoutedEventArgs e)
        {
            // Активация активной страницы из настроек
            //
            SenderPage = PEWindow.curPage;

            if (SenderPage != null)
            {
                PEWindow.ShowPopup("Активирована страница:\n" + SenderPage.Name);
                PEWindow.Close();
            }
            else
            {
                PEWindow.ShowPopup("Пусто");
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        { NIcon.Dispose(); }

        private void Button_sendClear_Click(object sender, RoutedEventArgs e)
        { DSender.SendClearCode(); }

        private void Button_singleSend_Click_1(object sender, RoutedEventArgs ev)
        {
            try
            {
                DSender.Send(SenderPage);
            }
            catch (Exception e)
            {
                MessageBox.Show("Ошибка одиночной отправки\n" + e.Message);
            }
        }
    }
}
