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

namespace CoffeeMaker_Client.ViewModel
{
    public class CoffeeMakerViewModel : INotifyPropertyChanged

    {
        #region Private Members
        private TcpService tcp = null;
        private string _sendMsg;
        private List<DrinkList> _drinkItems;
        private ObservableCollection<Btn> _buttonList;

        //DecoView
        private string _coffeeName = "";
        private string _sumPrice = "";
        private int _temperatureSelectIdx;
        private int _sizeSelectIdx;
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
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("SendMsg"));
            }
        }
        public ObservableCollection<Btn> ButtonList
        {
            get { return _buttonList; }
            set
            {
                _buttonList = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("ButtonList"));
            }
        }
        public string CoffeeName
        {
            get { return _coffeeName; }
            set
            {
                _coffeeName = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("CoffeeName"));
            }
        }
        public string SumPrice
        {
            get { return _sumPrice; }
            set
            {
                _sumPrice = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("SumPrice"));
            }
        }
        public int TemperatureSelectIdx
        {
            get { return _temperatureSelectIdx; }
            set
            {
                _temperatureSelectIdx = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("TemperatureSelectIdx"));
            }
        }
        public int SizeSelectIdx
        {
            get { return _sizeSelectIdx; }
            set
            {
                _sizeSelectIdx = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("SizeSelectIdx"));
            }
        }
        public ObservableCollection<TB> OptionList
        {
            get { return _optionList; }
            set
            {
                _optionList = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("OptionList"));
            }
        }
        public List<DrinkList> DrinkItems
        {
            get { return _drinkItems; }
            set
            {
                _drinkItems = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("DrinkItems"));
            }
        }
        #endregion

        #region 생성자
        public CoffeeMakerViewModel()
        {
            //tcp = new TcpService();
            //Database.Database database = new Database.Database();
            ButtonList = new ObservableCollection<Btn>();
            _temperatureSelectIdx = 0;
            _sizeSelectIdx = 0;
            OptionList = new ObservableCollection<TB>();
            TestCode();
            DrinkItems = new List<DrinkList>();
            //TestItem();
        }
        #endregion

        #region Command
        public ICommand SendMessageCommand => new RelayCommand(SendMessage);
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
        public void SendMessage(object parameter)
        {
            tcp.SendMessage(SendMsg);
        }
        public void OKExecute(object parameter)
        {
            AddTreeViewItem(_drink);
            window.Close();
        }
        public void CancelExecute(object parameter)
        {
            window.Close();
        }
        public void TempSelectionExecute(object parameter)
        {
            var test = parameter as ListBox;
            if (test.SelectedIndex == 0)
            {
                _drink.AddDeco(new Deco() { Name = "HOT", Price = 0 });
                _drink.DeleteDeco("ICE");
            }
            else
            {
                _drink.AddDeco(new Deco() { Name = "ICE", Price = 500 });
                _drink.DeleteDeco("HOT");
            }
            SetSumPrice( _drink.Price);
        }
        public void SizeSelectionExecute(object parameter)
        {
            var test = parameter as ListBox;
            if (test.SelectedIndex == 0)
            {
                _drink.AddDeco(new Deco() { Name = "TALL", Price = 0 });
                _drink.DeleteDeco("GRANDE");
                _drink.DeleteDeco("VENTI");
            }
            else if(test.SelectedIndex == 1)
            {
                _drink.AddDeco(new Deco() { Name = "GRANDE", Price = 1000 });
                _drink.DeleteDeco("TALL");
                _drink.DeleteDeco("VENTI");
            }
            else
            {
                _drink.AddDeco(new Deco() { Name = "VENTI", Price = 1500 });
                _drink.DeleteDeco("TALL");
                _drink.DeleteDeco("GRANDE");
            }
            SetSumPrice(_drink.Price);
        }
        #endregion

        #region IEventHandler
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region EventMethods
        #endregion

        #region Methods
        private void TestCode()
        {
            for (int i = 0; i < 10; i++)
            {
                Btn _menuBtn;
                ButtonStore store = new ButtonStore();
                _menuBtn = store.CreateBtn(BtnType.Sub, "아메리카노", 1500);
                _menuBtn.Height = 100;
                _menuBtn.Width = 200;
                _menuBtn.Click += OnClick;
                ButtonList.Add(_menuBtn);
            }
        }
        private void AddTreeViewItem(Drink drink)
        {
            foreach (DrinkList item in DrinkItems)
            {
                if(item.MenuName == drink.Name)
                {
                    item.DrinkItems.Add(new DrinkList { MenuName = drink.Name, Quantity = 1, Price = drink.Price, Option = drink.Option });
                    item.Quantity = item.DrinkItems.Count;
                    return;
                } 
            }
            //
            DrinkItems.Add
            (
                new DrinkList
                {
                    Number = DrinkItems.Count.ToString(),
                    MenuName = drink.Name,
                    Quantity = 1,
                    DrinkItems = new List<DrinkList>()
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
        private void TestItem()
        {
            DrinkItems = new List<DrinkList>()
            {
                new DrinkList
                {
                    Number = "0",
                    MenuName = "Coding",
                    Quantity = 4,
                    Price = 10000,
                    Option = "It pays the bills",
                    DrinkItems = new List<DrinkList>()
                    {
                        new DrinkList { MenuName = "Write", Quantity = 2, Option = "C# or go home" },
                        new DrinkList { MenuName = "Compile", Quantity = 1, Option = "WTB: SSD" },
                        new DrinkList { MenuName = "Test", Quantity = 1, Option = "Works on my machine" },
                    },
                },
                new DrinkList
                {
                    MenuName = "Meetings",
                    Quantity = 3,
                    Option = "A necessary evil",
                    DrinkItems = new List<DrinkList>()
                    {
                        new DrinkList { MenuName = "Boring", Quantity = 1, Option = "Zzzzzz" },
                        new DrinkList { MenuName = "Gossipy", Quantity = 1, Option = "Oh no he didn't!" },
                        new DrinkList { MenuName = "Useful", Quantity = 1, Option = "Right away, boss" },
                    },
                },
                new DrinkList
                {
                    MenuName = "Communicate",
                    Quantity = 3,
                    Option = "No man is an island",
                    DrinkItems = new List<DrinkList>()
                    {
                        new DrinkList { MenuName = "Email", Quantity = 1, Option = "So much junk mail" },
                        new DrinkList { MenuName = "Blogs", Quantity = 1, Option = "blogs.msdn.com/delay" },
                        new DrinkList { MenuName = "Twitter", Quantity = 1, Option = "RT: Nothing to report" },
                    },
                },
                new DrinkList
                {
                    MenuName = "Eating",
                    Quantity = 2,
                    Option = "Fuel for the body",
                    DrinkItems = new List<DrinkList>()
                    {
                        new DrinkList { MenuName = "Lunch", Quantity = 1, Option = "Bag lunch from home" },
                        new DrinkList
                        {
                            MenuName = "Snack",
                            Quantity = 2,
                            Option = "Still hungry",
                            DrinkItems = new List<DrinkList>()
                            {
                                new DrinkList { MenuName = "Fruit", Quantity = 1, Option = "Good for you" },
                                new DrinkList { MenuName = "Candy", Quantity = 1, Option = "Yummy!" },
                            },
                        },
                    },
                },
            };
        }
        private void OnClick(object sender, RoutedEventArgs e)
        {
            SubBtn item = sender as SubBtn;
            //Drink.Add(new Model.Drink() { Name = item.Name, Price = item.Price, OrderNo = Drink.Count.ToString() } );

            window = new Window
            {
                Title = "",
                Content = new View.DecoView(this)
            };
            window.Height = 600;
            window.Width = 500;
            AddOptionItems(item.Content.ToString(), item.Price);
            window.ShowDialog();
        }
        private void AddOptionItems(string name, int price)
        {
            CreateDrink(name, price);
            
            for (int i = 0; i < 10; i++)
            {
                TB tb;
                TextBlockStore tbStore = new TextBlockStore();
                tb = tbStore.CreateTB("Deco", 500); 
                OptionList.Add(tb);
            }
        }
        private void CreateDrink(string name, int price)
        {
            _drink = new Drink(name, price);
            _drink.AddDeco(new Deco() { Name = "HOT", Price = 0 });
            _drink.AddDeco(new Deco() { Name = "TALL", Price = 0 });

            CoffeeName = $"Menu : {_drink.Name}";
            SetSumPrice(_drink.Cost);
        }
        private void SetSumPrice(int cost)
        {
            SumPrice = $"Price : {cost}";
        }
        #endregion

        #region IDispose

        #endregion
    }
}
