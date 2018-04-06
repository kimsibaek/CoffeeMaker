using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using CoffeeMaker_Client.Model;
using CoffeeMaker_Client.TCP;
using CoffeeMaker_Client.ViewModel.Class;
using System.Windows.Controls;
using CoffeeMaker_Client.ButtonFactory;
using CoffeeMaker_Client.TextBlockFactory;
using System.Collections.Generic;
using System.ComponentModel;
using CoffeeMaker_Client.Binding;
using System;
using CoffeeMaker_Client.Query;
using System.Data;
using CoffeeMaker.Common;
using CoffeeMaker.Common.JsonFile;

namespace CoffeeMaker_Client.ViewModel
{
    public class CoffeeMakerViewModel : BaseViewModel

    {
        #region Private Members
        private Database.Database database;
        private TcpService tcp = null;
        private string _sendMsg;
        private ObservableCollection<DrinkList> _drinkItems;
        private ObservableCollection<Btn> _buttonList;
        private ObservableCollection<TB> _mainMenuList;
        private double _paidTotal;
        private int _discount;
        private double _total;
        private RelayCommand _payCommand;
        private RelayCommand _DeleteCommand;
        private RelayCommand _DiscountCommand;
        //DecoView
        private string _coffeeName = "";
        private string _sumPrice = "";
        private Window window;
        private ObservableCollection<TB> _optionList;
        private RelayCommand _OKCommand;
        private RelayCommand _CancelCommand;
        private Drink _drink;
        private ObservableCollection<TB> _optionSelectedItems;
        #endregion

        #region Properties
        public string SendMsg
        {
            get { return _sendMsg; }
            set
            {
                _sendMsg = value;
                OnPropertyChanged(nameof(SendMsg));
            }
        }
        public ObservableCollection<DrinkList> DrinkItems
        {
            get { return _drinkItems; }
            set
            {
                _drinkItems = value;
                OnPropertyChanged(nameof(DrinkItems));
            }
        }
        public ObservableCollection<Btn> ButtonList
        {
            get { return _buttonList; }
            set
            {
                _buttonList = value;
                OnPropertyChanged(nameof(ButtonList));
            }
        }
        public ObservableCollection<TB> MainMenuList
        {
            get { return _mainMenuList; }
            set
            {
                _mainMenuList = value;
                OnPropertyChanged(nameof(MainMenuList));
            }
        }
        public double PaidTotal
        {
            get { return _paidTotal; }
            set
            {
                _paidTotal = value;
                OnPropertyChanged(nameof(PaidTotal));
            }
        }
        public int Discount
        {
            get { return _discount; }
            set
            {
                _discount = value;
                OnPropertyChanged(nameof(Discount));
            }
        }
        public double Total
        {
            get { return _total; }
            set
            {
                _total = value;
                OnPropertyChanged(nameof(Total));
            }
        }
        public string CoffeeName
        {
            get { return _coffeeName; }
            set
            {
                _coffeeName = value;
                OnPropertyChanged(nameof(CoffeeName));
            }
        }
        public string SumPrice
        {
            get { return _sumPrice; }
            set
            {
                _sumPrice = value;
                OnPropertyChanged(nameof(SumPrice));
            }
        }
        public ObservableCollection<TB> OptionList
        {
            get { return _optionList; }
            set
            {
                _optionList = value;
                OnPropertyChanged(nameof(OptionList));
            }
        }
        public ObservableCollection<TB> OptionSelectedItems
        {
            get { return _optionSelectedItems; }
            set
            {
                _optionSelectedItems = value;
                OnPropertyChanged(nameof(OptionSelectedItems));
                SetPrice();
                if (_drink != null)
                {
                    SetSumPrice(_drink.Price);
                }
            }
        }
        #endregion

        #region 생성자
        public CoffeeMakerViewModel()
        {
            tcp = new TcpService();
            database = new Database.Database();
            _paidTotal = 0;
            _discount = 0;
            _total = 0;
            ButtonList = new ObservableCollection<Btn>();
            OptionList = new ObservableCollection<TB>();
            OptionSelectedItems = new ObservableCollection<TB>();
            MainMenuList = new ObservableCollection<TB>();
            SetMenu();
            DrinkItems = new ObservableCollection<DrinkList>();
        }
        #endregion

        #region Command
        //CoffeeMakerClientView
        public ICommand PayCommand
        {
            get
            {
                if (_payCommand == null)
                {
                    _payCommand = new RelayCommand(PayExecute);
                }
                return _payCommand;
            }
        }
        public ICommand DeleteCommand
        {
            get
            {
                if (_DeleteCommand == null)
                {
                    _DeleteCommand = new RelayCommand(DeleteExecute);
                }
                return _DeleteCommand;
            }
        }
        public ICommand DiscountCommand
        {
            get
            {
                if (_DiscountCommand == null)
                {
                    _DiscountCommand = new RelayCommand(DiscountExecute);
                }
                return _DiscountCommand;
            }
        }
        //DecoView
        public ICommand OKCommand
        {
            get
            {
                if (_OKCommand == null)
                {
                    _OKCommand = new RelayCommand(OKExecute);
                }
                return _OKCommand;
            }
        }
        public ICommand CancelCommand
        {
            get
            {
                if (_CancelCommand == null)
                {
                    _CancelCommand = new RelayCommand(CancelExecute);
                }
                return _CancelCommand;
            }
        }
        public ICommand TempSelectionChanged
        {
            get
            {
                return new RelayCommand(TempSelectionExecute);
            }
        }
        public ICommand SizeSelectionChanged
        {
            get
            {
                return new RelayCommand(SizeSelectionExecute);
            }
        }
        public ICommand DecoSelectionChanged
        {
            get
            {
                return new RelayCommand(DecoSelectionExecute);
            }
        }
        #endregion

        #region Command Method
        public void PayExecute(object parameter)
        {
            JsonOrderHistory data = new JsonOrderHistory();
            data.AccountInfo.AccountNo = "100014";
            data.AccountInfo.Receive = "2500"; // PaidTotal.ToString();
            data.AccountInfo.Discount = "0"; // Discount.ToString();
            data.AccountInfo.Total = "2500"; // Total.ToString();
            data.AccountInfo.UserNo = "0";
            OrderHistoryItem item = new OrderHistoryItem();
            item.DecoList = new List<string>();
            item.AccountNo = "100014";
            item.OrderNo = "10006";
            item.DrinkNo = "1001";
            item.Price = "2500";
            item.DecoList.Add("HOTt");
            item.DecoList.Add("103");
            item.DecoList.Add("106");
            data.OrderHistoryList.Add(item);
            SendMsg = JsonExtention.Serialize(data);
            string result = tcp.SendMessage(SendMsg);

        }
        public void DeleteExecute(object parameter)
        {
            TreeView treeview = parameter as TreeView;
            if (treeview.SelectedItem == null)
            {
                return;
            }
            if(MessageBox.Show("선택한 항목을 삭제하시겠습니까?", "CMS", MessageBoxButton.OKCancel, MessageBoxImage.Question) == MessageBoxResult.OK)
            {
                if (parameter != null)
                {
                    DeleteTreeViewItem(treeview.SelectedItem as DrinkList);
                    SetPaidTotal();
                }
            }
        }
        public void DiscountExecute(object parameter)
        {
            if (PaidTotal > Discount)
            {
                Discount += 500;
                if (PaidTotal < Discount)
                {
                    Discount = (int)PaidTotal;
                }
            }
        }
        public void OKExecute(object parameter)
        {
            AddTreeViewItem(_drink);
            SetPaidTotal();
            window.Close();
            window = null;
        }
        public void CancelExecute(object parameter)
        {
            window.Close();
            window = null;
        }
        public void TempSelectionExecute(object parameter)
        {
            var test = parameter as ListBox;
            if (test.SelectedIndex == 0)
            {
                DecoTB hot = new DecoTB("HOT", 0);
                hot.CreateDeco(_drink);
                DecoTB ice = new DecoTB("ICE", 500);
                ice.DeleteDeco(_drink);
                //_drink.AddDeco(new DecoTB("HOT", 0));
                //_drink.DeleteDeco("ICE");
            }
            else
            {
                DecoTB hot = new DecoTB("HOT", 0);
                hot.DeleteDeco(_drink);
                DecoTB ice = new DecoTB("ICE", 500);
                ice.CreateDeco(_drink);
                //_drink.AddDeco(new DecoTB("ICE", 500));
                //_drink.DeleteDeco("HOT");
            }
            SetSumPrice( _drink.Price);
        }
        public void SizeSelectionExecute(object parameter)
        {
            var test = parameter as ListBox;
            if (test.SelectedIndex == 0)
            {
                DecoTB tall = new DecoTB("TALL", 0);
                tall.CreateDeco(_drink);
                DecoTB grande = new DecoTB("GRANDE", 1000);
                grande.DeleteDeco(_drink);
                DecoTB venti = new DecoTB("VENTI", 1500);
                venti.DeleteDeco(_drink);

                //_drink.AddDeco(new DecoTB("TALL", 0));
                //_drink.DeleteDeco("GRANDE");
                //_drink.DeleteDeco("VENTI");
            }
            else if(test.SelectedIndex == 1)
            {
                DecoTB tall = new DecoTB("TALL", 0);
                tall.DeleteDeco(_drink);
                DecoTB grande = new DecoTB("GRANDE", 1000);
                grande.CreateDeco(_drink);
                DecoTB venti = new DecoTB("VENTI", 1500);
                venti.DeleteDeco(_drink);

                //_drink.AddDeco(new DecoTB("GRANDE", 1000));
                //_drink.DeleteDeco("TALL");
                //_drink.DeleteDeco("VENTI");
            }
            else
            {
                DecoTB tall = new DecoTB("TALL", 0);
                tall.DeleteDeco(_drink);
                DecoTB grande = new DecoTB("GRANDE", 1000);
                grande.DeleteDeco(_drink);
                DecoTB venti = new DecoTB("VENTI", 1500);
                venti.CreateDeco(_drink);

                //_drink.AddDeco(new DecoTB("VENTI", 1500));
                //_drink.DeleteDeco("TALL");
                //_drink.DeleteDeco("GRANDE");
            }
            SetSumPrice(_drink.Price);
        }
        public void DecoSelectionExecute(object parameter)
        {
            var item = parameter as ListBox;
            if(item == null)
            {
                return;
            }
            foreach (var deco in item.Items)
            {
                var d = deco as DecoTB;
                d.DeleteDeco(_drink);
            }
            foreach (var deco in item.SelectedItems)
            {
                var d = deco as DecoTB;
                d.CreateDeco(_drink);
            }
            SetSumPrice(_drink.Price);
            //deco.CreateDeco(_drink);
        }
        #endregion

        #region IEventHandler
        private void OnClick(object sender, RoutedEventArgs e)
        {
            window = new Window
            {
                Title = "",
                Content = new View.DecoView(this)
            };
            window.Height = 600;
            window.Width = 500;

            SubBtn item = sender as SubBtn;
            AddOptionItems(item.Content.ToString(), item.Price);

            window.ShowDialog();
        }
        #endregion

        #region EventMethods
        #endregion

        #region Methods
        private void SetMenu()
        {
            DBQuery query = new DBQuery();
            DataTable table = database.ExecuteQuery(query.SelectDrink());

            ButtonStore store = new ButtonStore();
            foreach (DataRow item in table.Rows)
            {
                Btn _menuBtn;
                store.CreateBtn(out _menuBtn, BtnType.Sub, item["NAME"].ToString(), int.Parse(item["PRICE"].ToString()));
                _menuBtn.Height = 100;
                _menuBtn.Width = 200;
                _menuBtn.Click += OnClick;
                ButtonList.Add(_menuBtn);
            }

            //ButtonStore store = new ButtonStore();

            //Btn _menuBtn0;
            //store.CreateBtn(out _menuBtn0, BtnType.Sub, "에스프레소", 1000);
            //_menuBtn0.Height = 100;
            //_menuBtn0.Width = 200;
            //_menuBtn0.Click += OnClick;
            //ButtonList.Add(_menuBtn0);

            //Btn _menuBtn1;
            //store.CreateBtn(out _menuBtn1, BtnType.Sub, "아메리카노", 1500);
            //_menuBtn1.Height = 100;
            //_menuBtn1.Width = 200;
            //_menuBtn1.Click += OnClick;
            //ButtonList.Add(_menuBtn1);

            //Btn _menuBtn2;
            //store.CreateBtn(out _menuBtn2, BtnType.Sub, "카페라떼", 2500);
            //_menuBtn2.Height = 100;
            //_menuBtn2.Width = 200;
            //_menuBtn2.Click += OnClick;
            //ButtonList.Add(_menuBtn2);

            //Btn _menuBtn3;
            //store.CreateBtn(out _menuBtn3, BtnType.Sub, "카라멜마끼아또", 3000);
            //_menuBtn3.Height = 100;
            //_menuBtn3.Width = 200;
            //_menuBtn3.Click += OnClick;
            //ButtonList.Add(_menuBtn3);

            //Btn _menuBtn4;
            //store.CreateBtn(out _menuBtn4, BtnType.Sub, "바닐라라떼", 3500);
            //_menuBtn4.Height = 100;
            //_menuBtn4.Width = 200;
            //_menuBtn4.Click += OnClick;
            //ButtonList.Add(_menuBtn4);
        }
        private void AddTreeViewItem(Drink drink)
        {
            for (int i = 0; i < DrinkItems.Count; i++)
            {
                if (DrinkItems[i].MenuName == drink.Name)
                {
                    DrinkItems[i].DrinkItems.Add(new DrinkList { MenuName = drink.Name, Quantity = 1, Price = drink.Price, Option = drink.Option });
                    DrinkItems[i].Quantity = DrinkItems[i].DrinkItems.Count;
                    DrinkItems[i].Price += drink.Price;
                    return;
                }
            }
            
            //New Item
            DrinkItems.Add
            (
                new DrinkList
                {
                    Number = DrinkItems.Count.ToString(),
                    MenuName = drink.Name,
                    Quantity = 1,
                    Price = drink.Price,
                    DrinkItems = new ObservableCollection<DrinkList>()
                    {
                        new DrinkList
                        {
                            MenuName = drink.Name,
                            Quantity = 1,
                            Price = drink.Price,
                            Option = drink.Option,
                        }
                    }
                }
            );
        }
        private void DeleteTreeViewItem(DrinkList drinkList)
        {
            if (drinkList.Number != null)
            {
                DrinkItems.Remove(drinkList);
                return;
            }
            else
            {
                for (int i = 0; i < DrinkItems.Count; i++)
                {
                    if(DrinkItems[i].MenuName == drinkList.MenuName)
                    {
                        DrinkItems[i].DrinkItems.Remove(drinkList);
                        DrinkItems[i].Quantity -= 1;
                        DrinkItems[i].Price -= drinkList.Price;
                        if(DrinkItems[i].Quantity == 0)
                        {
                            DrinkItems.RemoveAt(i);
                        }
                        return;
                    }
                }
            }
        }
        private void AddOptionItems(string name, int price)
        {
            OptionList.Clear();
            CreateDrink(name, price);
            DBQuery query = new DBQuery();
            DataTable table = database.ExecuteQuery(query.SelectDeco());

            foreach (DataRow item in table.Rows)
            {
                TB tb;
                TextBlockStore tbStore = new TextBlockStore();
                tbStore.CreateTB(out tb, TBType.Deco, item["NAME"].ToString(), int.Parse(item["PRICE"].ToString()));
                //tbStore.CreateTB(out tb, TBType.Deco, "Deco", 500); 
                OptionList.Add(tb);
            }

            //TextBlockStore tbStore = new TextBlockStore();

            //TB tb0;
            //tbStore.CreateTB(out tb0, TBType.Deco, "Shot", 500);
            //OptionList.Add(tb0);

            //TB tb1;
            //tbStore.CreateTB(out tb1, TBType.Deco, "휘핑크림", 1000);
            //OptionList.Add(tb1);

            //TB tb2;
            //tbStore.CreateTB(out tb2, TBType.Deco, "시나몬", 1000);
            //OptionList.Add(tb2);

            //TB tb;
            //tbStore.CreateTB(out tb, TBType.Deco, "초코", 500);
            //OptionList.Add(tb);
        }
        private void CreateDrink(string name, int price)
        {
            _drink = new Drink(name, price);
            DecoTB hot = new DecoTB("HOT", 0);
            hot.CreateDeco(_drink);
            DecoTB tall = new DecoTB("TALL", 0);
            tall.CreateDeco(_drink);
            //_drink.AddDeco(new DecoTB("HOT", 0));
            //_drink.AddDeco(new DecoTB("TALL", 0));

            CoffeeName = $"Menu : {_drink.Name}";
            SetSumPrice(_drink.Cost);
        }
        private void SetSumPrice(int cost)
        {
            SumPrice = $"Price : {cost}";
        }
        private void SetPaidTotal()
        {
            PaidTotal = 0;
            foreach (DrinkList item in DrinkItems)
            {
                PaidTotal += item.Price;
            }
            Total = PaidTotal - Discount;
            Total = Math.Round(Total / 10) * 10f;
            if (Total < 0)
            {
                Total = 0;
            }
        }
        private void SetPrice()
        {
            //try
            //{
            //    _drink.Price = _drink.Cost;
            //    foreach (TB item in _drink.OptionList)
            //    {
            //        _drink.Price += item.Price;
            //    }
            //}
            //catch (Exception)
            //{
            //}
           
        }
        #endregion

        #region IDispose

        #endregion
    }
}
