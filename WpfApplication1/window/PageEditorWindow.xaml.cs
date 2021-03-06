﻿using System; using System.Collections.Generic; using System.Linq; using System.Text; using System.Windows; using System.Windows.Controls; using System.Windows.Data; using System.Windows.Documents; using System.Windows.Input; using System.Windows.Media; using System.Windows.Media.Imaging; using System.Windows.Shapes; using System.Xml;  using ArdClock.ArdPage;  using Lib; using Lib.HelpingClass;  namespace ArdClock.window {
    /// <summary>     /// Логика взаимодействия для Window1.xaml     /// </summary>     ///  

    public partial class PageEditorWindow : Window     {
        /// <summary>Список страниц/// </summary>         public List<APage> ListPages { get; private set; }

        /// <summary>Список настроек элементов на выбранной странице/// </summary>         private List<AbstrUIBase> ListUIControls;          /// <summary>Выбранная страница/// </summary>         public APage CurPage { get; private set; }         private int _curPageIndex;         private TextBox _curPageName;          public static readonly string pathToXML = System.Environment.CurrentDirectory + "\\ListPages.xml";          System.Windows.Threading.DispatcherTimer timerPopup;          public PageEditorWindow()         {             InitializeComponent();             button_Activate.Click += (s, e) => Save_Click(s, e);              timerPopup = new System.Windows.Threading.DispatcherTimer();             timerPopup.Tick += ClosePopup;              ListUIControls = new List<AbstrUIBase>();

            elementsPageStackPanel.ContextMenuOpening += MouseRightButton_Click;             SetContextMenuWith(null);              UpdateListPage();             CurPage = new APage();             _curPageName = CreatePageNameTextBox(CurPage);         }

        // Вывод интефейса для редактирования элементов 
        // страницы
        private void ListBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)         {
            _curPageIndex = list_page_name.SelectedIndex != -1 ?                 list_page_name.SelectedIndex : _curPageIndex;
                         UpdateListPageEl();         }          public void SoftUpdate()         {
            // Загружает информацию о элементах на странице
            // из сохранённого списка
            //

            if (CurPage != null)
            {                 ClearListPageEl();                  _curPageName = CreatePageNameTextBox(CurPage);                  elementsPageStackPanel.Children.Add(_curPageName);                  for (int i = 0; i < ListUIControls.Count; i++)                 {                     AbstrUIBase el = ListUIControls[i];                     el.SetID(i);                     el.PageContainer.Background =                         (i % 2 == 0) ? Brushes.WhiteSmoke : Brushes.LightGray;                      elementsPageStackPanel.Children.Add(ListUIControls[i]);                     elementsPageStackPanel.Children.Add(                     UIGenerateHelping.NewSeparator(1, Brushes.Black));                 }             }         }         public void UpdateListPageEl(UIBaseEl new_el = null)         {
            // Загружает информацию о элементах на странице
            // напрямую из сохранённой страницы
            //
            ClearListPageEl(); 
            if (list_page_name.Items.Count <= _curPageIndex)
            {
                return;
            }              APage editPage = ListPages[_curPageIndex];              if (new_el != null)                 editPage.Elements.Add(new_el.CompileElement());              ListUIControls.Clear();             for (int i = 0; i < editPage.Elements.Count; i++)             {                 var el = editPage.Elements[i];                 CreateNewUIel(el);             }              CurPage = editPage;              SoftUpdate();         }         public void ClearListPageEl()
        {
            elementsPageStackPanel.Children.Clear();
            System.GC.Collect();         }         public void ExpandListPageEl(bool expand)
        {
            ListUIControls.ForEach((e) => (e as UIBaseEl).SetExpand(expand));
        }

        // Загрузить список страниц заново
        public void UpdateListPage()
        {
            ListPages = PageElCenter.LoadPageListFromXML(pathToXML);             list_page_name.ItemsSource = ListPages;         }         private void CreateNewUIel(AbstrPageEl el)
        {             AbstrUIBase UIel = PageElCenter.TryGenUiControl(el);             AddUItoList(UIel);         }

        private void CreateNewUIel(int ind)
        {             AbstrUIBase UIel = PageElCenter.TryGenUiControl(ind);             AddUItoList(UIel);         }          private void AddUItoList(AbstrUIBase UIel)         {             if (UIel != null)             {                 UIel.DelClick += UIPageEl_DelClick;                 UIel.SetID(ListUIControls.Count + 1);                 UIel.Drop += UIElementDropEvent;                 ListUIControls.Add(UIel);             }         }         private TextBox CreatePageNameTextBox(APage page)          {
            TextBox tmp_TB =  new TextBox();
            tmp_TB .Text = page.Name;
            tmp_TB .Width = 256;
            tmp_TB.TextAlignment = TextAlignment.Center;              return tmp_TB;         }          private void Swap(int u1, int u2)
        {             AbstrUIBase u_tmp = ListUIControls[u1];             ListUIControls[u1] = ListUIControls[u2];             ListUIControls[u2] = u_tmp;             SoftUpdate();         }         private void SavePageList()         {             PageElCenter.WritePageListToXML(ListPages, pathToXML);         }


        //
        // Обработка поднимающихся событий 
        //
        public void MouseRightButton_Click(object sender, RoutedEventArgs e)
        {
            if (e.Source is AbstrUIBase)
                SetContextMenuWith(e.Source as AbstrUIBase);             else                 SetContextMenuWith(null);         }         private void SetContextMenuWith(AbstrUIBase UIel)
        {             ContextMenu cm = new ContextMenu();              MenuItem miAdd = new MenuItem { Header = "Добавить" };             MenuItem miSave = new MenuItem { Header = "Сохранить" };             MenuItem miExpand = new MenuItem { Header = "Свернуть список" };              miAdd.MouseEnter += MenuItemAdd_MouseEnter;             miSave.Click += Save_Click;             miExpand.Click += (s, ev) => ExpandListPageEl(false);                          if (UIel != null)
            {
                MenuItem miDel = new MenuItem();
                MenuItem miName = new MenuItem();

                miDel.Header = "Удалить";
                miName.Header = UIel.NamePageEl;

                miDel.Click += UIel.RaiseDelClick;

                miName.IsEnabled = false;

                cm.Items.Add(miName);
                cm.Items.Add(miDel);
                cm.Items.Add(new Separator());
                cm.Items.Add(miExpand);
            }              cm.Items.Add(miAdd);             cm.Items.Add(miSave);

            elementsPageStackPanel.ContextMenu = cm;
        }
        //
        // Events
        //
        private void Save_Click(object sender, RoutedEventArgs e)         {             List<AbstrPageEl> new_elements = new List<AbstrPageEl>();              foreach (var UIel in ListUIControls)             {                 new_elements.Add(UIel.CompileElement());             }              CurPage = new APage(_curPageName.Text, CurPage.ID, new_elements);              if (list_page_name.Items.Count > _curPageIndex)                 ListPages[_curPageIndex] = CurPage;              SavePageList();
            UpdateListPage();             UpdateListPageEl();              ShowPopup(String.Format(                 "Сохранено: {0} эл.", new_elements.Count.ToString()));         }          private void UIPageEl_DelClick(object sender, EventArgs e)         {             ListUIControls.Remove((AbstrUIBase)sender);             SoftUpdate();         }          private void UIElementDropEvent(object sender, EventArgs e)
        {             if (sender is AbstrUIBase && (e as DragDropArgs).drop is AbstrUIBase)
            {                 Swap((sender as AbstrUIBase).ID, ((e as DragDropArgs).drop as AbstrUIBase).ID);
            }         }


        //
        // Context Menu events
        //
        /// <summary>
        /// При наведении на кнопку "добавить" в контекстном меню
        /// обновляет список доступных элементов
        /// </summary>         private void MenuItemAdd_MouseEnter(object sender, RoutedEventArgs e)         {             List<MenuItem> lm = new List<MenuItem>();              foreach (KeyValuePair<int, string> kv in PageElCenter.GetDictNames())             {                 MenuItem mi = new MenuItem();                  mi.Header = kv.Value;                 mi.Click += MenuItemAddPageEl_Click;                  lm.Add(mi);             }              ((MenuItem)sender).ItemsSource = lm;         }           public void MenuItemAddPageEl_Click(object sender, RoutedEventArgs e)         {             string nm = ((MenuItem)sender).Header.ToString();              foreach (KeyValuePair<int, string> kv in PageElCenter.GetDictNames())             {                 if (nm == kv.Value)                 {                     CreateNewUIel(kv.Key);                     SoftUpdate();                     break;                 }             }         }

        // Context Menu: Update
        /// <summary>
        /// Заново загружает список страниц
        /// </summary>         public void MenuUpdate(object sender, RoutedEventArgs e)         {
            SavePageList();
            UpdateListPage();             UpdateListPageEl();         }          private void MenuItemListPageUpdate_Click(object sender, RoutedEventArgs e)         {             ClearListPageEl();             UpdateListPage();         }          private void MenuItemListPageNew_Click(object sender, RoutedEventArgs e)
        {             Random rnd = new Random();             ListPages.Add(new APage("NewPage", rnd.Next(), new List<AbstrPageEl>()));              SavePageList();
            UpdateListPage();             UpdateListPageEl();         }         private void MenuItemListPageDel_Click(object sender, RoutedEventArgs e)
        {
             if (_curPageIndex != -1)
            {                 ListPages.RemoveAt(_curPageIndex);
                _curPageIndex = Math.Max(--_curPageIndex, 0);

                SavePageList();

                UpdateListPage();
                UpdateListPageEl();             }         }

        //
        //
        //

        //
        // Popup logic
        //
        public void ShowPopup(String text, double sec = 1)         {             popupTextBox.Opacity = 0.8;             popupTextBox.Text = text;              popup1.IsOpen = true;              timerPopup.Interval = TimeSpan.FromSeconds(sec);             timerPopup.Start();         }          private void ClosePopup(object sender, EventArgs e)         {             timerPopup.Interval = TimeSpan.FromMilliseconds(50);             popupTextBox.Opacity *= 0.8;              if (popupTextBox.Opacity <= 0.2)             {                 popup1.IsOpen = false;                 popupTextBox.Opacity = 0.8;                 timerPopup.Stop();             }         }          private void popup1_MouseLeave(object sender, MouseEventArgs e)         {             timerPopup.Interval = TimeSpan.FromMilliseconds(500);             timerPopup.Start();         }          private void Popup1_MouseEnter(object sender, MouseEventArgs e)         {             popupTextBox.Opacity = 0.8;             timerPopup.Stop();         }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            ListPages.Clear();
            ListUIControls.Clear();
        }
    } } 