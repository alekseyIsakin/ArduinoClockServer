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
using System.Windows.Shapes;
using System.Xml;

using ArdClock.src;
using ArdClock.src.ArdPage;
using ArdClock.src.ArdPage.HelpingClass;
using ArdClock.src.UIGenerate;
using ArdClock.src.ArdPage.PageElements;
using ArdClock.src.XMLLoader;

using BaseLib;

namespace ArdClock.window
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    /// 


    public partial class PageEditorWindow : Window
    {
        public List<APage> pageList { get; private set; }
        private List<AbstrUIBase> UIControlList;
        public APage curPage { get; private set; }

        public string pathToXML = System.Environment.CurrentDirectory + "\\ListPages.xml";

        System.Windows.Threading.DispatcherTimer timerPopup;

        public PageEditorWindow()
        {
            InitializeComponent();
            button_Activate.Click += (s, e) => button_Save_Click(s, e);

            timerPopup = new System.Windows.Threading.DispatcherTimer();
            timerPopup.Tick += ClosePopup;

            pageList = Loader.LoadPageListFromXML(pathToXML);

            UIControlList = new List<AbstrUIBase>();

            if (pageList.Count > 0)
            {
                list_page_name.ItemsSource = pageList;
            }
        }

        // Вывод интефейса для редактирования элементов 
        // страницы
        private void listBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateListPageEl();
        }

        public void SoftUpdate()
        {
            // Загружает информацию из сохранённого списка
            //

            if (curPage != null) 
            {
                elementsPageStackPanel.Children.Clear();

                Label pageNameLabel = new Label();
                pageNameLabel.Content = curPage.Name;
                elementsPageStackPanel.Children.Add(pageNameLabel);

                for (int i = 0; i < UIControlList.Count; i++)
                {
                    AbstrUIBase el = UIControlList[i];
                    el.SetID(i);
                    el.Container.Background =
                        (i % 2 == 0) ? Brushes.WhiteSmoke : Brushes.LightGray;

                    elementsPageStackPanel.Children.Add(UIControlList[i].Container);
                    elementsPageStackPanel.Children.Add(
                    UIGenerateHelping.NewSeparator(1, Brushes.Black));
                }
            }
        }

        public void UpdateListPageEl(UIBaseEl new_el = null)
        {
            // Загружает информацию напрямую из сохранённой страницы
            //

            if (list_page_name.SelectedIndex == -1)
                return;

            APage editPage = pageList[list_page_name.SelectedIndex];

            elementsPageStackPanel.Children.Clear();

            if (new_el != null)
                editPage.Elements.Add(new_el.CompileElement());

            UIControlList.Clear();
            //var el in editPage.Elements
            for (int i = 0; i < editPage.Elements.Count; i++ )
            {
                var el = editPage.Elements[i];
                el.SetID(i);
                CreateNewUIel(el);
            }

            curPage = editPage;

            SoftUpdate();
        }

        private void CreateNewUIel(AbstrPageEl el) 
        {
            AbstrUIBase UIel = PageElCenter.TryGenUiControl(el);
            AddUItoList(UIel);
        }
        
        private void CreateNewUIel(int ind) 
        {
            AbstrUIBase UIel = PageElCenter.TryGenUiControl(ind);
            AddUItoList(UIel);
        }

        private void AddUItoList(AbstrUIBase UIel)
        {
            if (UIel != null)
            {
                UIel.DelClick += UIPageEl_DelClick;
                UIel.SetID(UIControlList.Count + 1);
                UIel.Drop += UIElementDropEvent;
                UIControlList.Add(UIel);
            }
        }

        private void Swap(int u1, int u2) 
        {
            AbstrUIBase u_tmp = UIControlList[u1];
            UIControlList[u1] = UIControlList[u2];
            UIControlList[u2] = u_tmp;
            SoftUpdate();
        }
        //
        // Events
        //

        private void button_Save_Click(object sender, RoutedEventArgs e)
        {
            List<AbstrPageEl> new_elements = new List<AbstrPageEl>();

            foreach (var UIel in UIControlList)
            {
                new_elements.Add(UIel.CompileElement());
            }

            if (new_elements.Count >= 1)
            {
                curPage = new APage(curPage.Name, curPage.ID, new_elements);

                if (list_page_name.SelectedIndex != -1)
                    pageList[list_page_name.SelectedIndex] = curPage;

                Writer.WritePageListToXML(pageList, pathToXML);
                UpdateListPageEl();

                ShowPopup(String.Format(
                    "Сохранено: {0} эл.", new_elements.Count.ToString()));
            }
            else { ShowPopup("Ничего не сохранено :("); }
        }

        private void UIPageEl_DelClick(object sender, EventArgs e)
        {
            UIControlList.Remove((AbstrUIBase)sender);
            SoftUpdate();
        }

        private void UIElementDropEvent(object sender, EventArgs e) 
        {
            if (sender is AbstrUIBase) 
            {
                Swap((sender as AbstrUIBase).ID, ((e as DragDropArgs).drop as AbstrUIBase).ID);
                //MessageBox.Show((sender as AbstrUIBase).ID.ToString() + "\n" +
                //                ((e as DragDropArgs).drop as AbstrUIBase).ID.ToString());
            }
        }
        //
        // Popup logic
        //
        public void ShowPopup(String text, double sec = 1)
        {
            popupTextBox.Opacity = 0.8;
            popupTextBox.Text = text;

            popup1.IsOpen = true;

            timerPopup.Interval = TimeSpan.FromSeconds(sec);
            timerPopup.Start();
        }

        private void ClosePopup(object sender, EventArgs e)
        {
            timerPopup.Interval = TimeSpan.FromMilliseconds(50);
            popupTextBox.Opacity *= 0.8;

            if (popupTextBox.Opacity <= 0.2)
            {
                popup1.IsOpen = false;
                popupTextBox.Opacity = 0.8;
                timerPopup.Stop();
            }
        }

        private void popup1_MouseLeave(object sender, MouseEventArgs e)
        {
            timerPopup.Interval = TimeSpan.FromMilliseconds(500);
            timerPopup.Start();
        }

        private void popup1_MouseEnter(object sender, MouseEventArgs e)
        {
            popupTextBox.Opacity = 0.8;
            timerPopup.Stop();
        }


        //
        // Context Menu events
        //

        // Context Menu: New Element
        private void MenuItemAdd_MouseEnter(object sender, RoutedEventArgs e)
        {
            List<MenuItem> lm = new List<MenuItem>();

            foreach (KeyValuePair<int, string> kv in PageElCenter.getDict())
            {
                MenuItem mi = new MenuItem();

                mi.Header = kv.Value;
                mi.Click += MenuItemAddPageEl_Click;

                lm.Add(mi);
            }

            ((MenuItem)sender).ItemsSource = lm;
        }

        public void MenuItemAddPageEl_Click(object sender, RoutedEventArgs e)
        {
            string nm = ((MenuItem)sender).Header.ToString();

            foreach (KeyValuePair<int, string> kv in PageElCenter.getDict())
            {
                if (nm == kv.Value)
                {
                    CreateNewUIel(kv.Key);
                    SoftUpdate();
                    break;
                }
            }
        }
        // Context Menu: Update
        public void MenuUpdate(object sender, RoutedEventArgs e)
        {
            button_Save_Click(this, null);
        }

        //
        //
        //
    }
}
