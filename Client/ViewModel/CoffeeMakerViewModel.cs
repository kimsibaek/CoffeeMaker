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

namespace CoffeeMaker_Client.ViewModel
{
    public class CoffeeMakerViewModel : BaseViewModel

    {
        #region Private Members
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
        
        #endregion

        #region 생성자
        public CoffeeMakerViewModel()
        {
            //tcp = new TcpService();
            //Database.Database database = new Database.Database();
            _paidTotal = 0;
            _discount = 0;
            _total = 0;
            ButtonList = new ObservableCollection<Btn>();
            OptionList = new ObservableCollection<TB>();
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
        #endregion

        #region Command Method
        public void PayExecute(object parameter)
        {
            //tcp.SendMessage(SendMsg);
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
                _drink.AddDeco(new TB() { Name = "HOT", Price = 0 });
                _drink.DeleteDeco("ICE");
            }
            else
            {
                _drink.AddDeco(new TB() { Name = "ICE", Price = 500 });
                _drink.DeleteDeco("HOT");
            }
            SetSumPrice( _drink.Price);
        }
        public void SizeSelectionExecute(object parameter)
        {
            var test = parameter as ListBox;
            if (test.SelectedIndex == 0)
            {
                _drink.AddDeco(new TB() { Name = "TALL", Price = 0 });
                _drink.DeleteDeco("GRANDE");
                _drink.DeleteDeco("VENTI");
            }
            else if(test.SelectedIndex == 1)
            {
                _drink.AddDeco(new TB() { Name = "GRANDE", Price = 1000 });
                _drink.DeleteDeco("TALL");
                _drink.DeleteDeco("VENTI");
            }
            else
            {
                _drink.AddDeco(new TB() { Name = "VENTI", Price = 1500 });
                _drink.DeleteDeco("TALL");
                _drink.DeleteDeco("GRANDE");
            }
            SetSumPrice(_drink.Price);
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
            //Main Menu
            TextBlockStore tStore = new TextBlockStore();
            for (int i = 0; i < 3; i++)
            {
                TB tb;
                tStore.CreateTB(out tb, TBType.Main, "Coffee");
                MainMenuList.Add(tb);
            }
            //Menu
            ButtonStore bStore = new ButtonStore();
            for (int i = 0; i < 10; i++)
            {
                Btn _menuBtn;
                bStore.CreateBtn(out _menuBtn, BtnType.Sub, "아메리카노", 1500);
                _menuBtn.Click += OnClick;
                ButtonList.Add(_menuBtn);
            }
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
            CreateDrink(name, price);
            
            for (int i = 0; i < 10; i++)
            {
                TB tb;
                TextBlockStore tbStore = new TextBlockStore();
                tbStore.CreateTB(out tb, TBType.Deco, "Deco", 500); 
                OptionList.Add(tb);
            }
        }
        private void CreateDrink(string name, int price)
        {
            _drink = new Drink(name, price);
            _drink.AddDeco(new TB() { Name = "HOT", Price = 0 });
            _drink.AddDeco(new TB() { Name = "TALL", Price = 0 });

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
        #endregion

        #region IDispose

        #endregion
    }
}
