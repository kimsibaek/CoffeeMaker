using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using CoffeeMaker_Client.Model;
using CoffeeMaker_Client.TCP;
using CoffeeMaker_Client.ViewModel.Class;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows.Controls.Primitives;
using CoffeeMaker_Client.ButtonFactory;
using CoffeeMaker_Client.TextBlockFactory;

namespace CoffeeMaker_Client.ViewModel
{
    public class CoffeeMakerViewModel

    {
        #region Private Members
        private Window window;
        private RelayCommand _drinkCommand;
        private RelayCommand _OKCommand;
        private RelayCommand _CancelCommand;
        private RelayCommand _selectCommand;
        private DrinkList[] _drinkItems;
        private ObservableCollection<Drink> _drink;
        private ObservableCollection<Btn> _buttonList;
        private string _information;
        private int _temperatureSelectIdx;
        private int _sizeSelectIdx;
        private ObservableCollection<TB> _optionList;
        private TextBlock _selectItem;
        private string _sendMsg;
        TcpService tcp = null;
        #endregion

        #region Properties
        public string SendMsg
        {
            get { return _sendMsg; }
            set { _sendMsg = value; }
        }

        public ObservableCollection<Drink> Drink
        {
            get { return _drink; }
            set { _drink = value; }
        }

        public ObservableCollection<Btn> ButtonList
        {
            get { return _buttonList; }
            set { _buttonList = value; }
        }
        public string Information
        {
            get { return _information; }
            set { _information = value; }
        }
        public int TemperatureSelectIdx
        {
            get { return _temperatureSelectIdx; }
            set { _temperatureSelectIdx = value; }
        }
        public int SizeSelectIdx
        {
            get { return _sizeSelectIdx; }
            set { _sizeSelectIdx = value; }
        }
        public ObservableCollection<TB> OptionList
        {
            get { return _optionList; }
            set { _optionList = value; }
        }
        public TextBlock SelectItem
        {
            get { return _selectItem; }
            set { _selectItem = value; }
        }
        public DrinkList[] DrinkItems
        {
            get { return _drinkItems; }
            set { _drinkItems = value; }
        }
        #endregion

        #region 생성자
        public CoffeeMakerViewModel()
        {
            //tcp = new TcpService();
            //Database.Database database = new Database.Database();
            Drink = new ObservableCollection<Drink>();
            ButtonList = new ObservableCollection<Btn>();
            _temperatureSelectIdx = 0;
            _sizeSelectIdx = 0;
            OptionList = new ObservableCollection<TB>();
            TestCode();
            //TestItem();
        }
        #endregion

        #region Command
        public ICommand Cafucino
        {
            get
            {
                if (_drinkCommand == null)
                {
                    _drinkCommand = new RelayCommand(CafucinoExecute, CanExecute);
                }
                return _drinkCommand;
            }
        }
        public ICommand Americano
        {
            get
            {
                if (_drinkCommand == null)
                {
                    _drinkCommand = new RelayCommand(AmericanoExecute, CanExecute);
                }
                return _drinkCommand;
            }

        }
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
        public ICommand SelectionChanged
        {
            get
            {
                if (_selectCommand == null)
                {
                    _selectCommand = new RelayCommand(SelectionExecute);
                }
                return _selectCommand;
            }
        }

        public ICommand SendMessageCommand => new RelayCommand(SendMessage);

        #endregion

        #region Command Method
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void CafucinoExecute(object parameter)
        {
            DrinkAdd(Constants.DrinkType.Cafucino);
        }

        public void AmericanoExecute(object parameter)
        {
            DrinkAdd(Constants.DrinkType.Americano);
        }
        public void OKExecute(object parameter)
        {
            var content = window.Content;
            View.DecoView view = content as View.DecoView;
            
            MessageBox.Show($"{TemperatureSelectIdx}, {SizeSelectIdx}, {view.Listboxitem.SelectedItems}");
        }
        public void CancelExecute(object parameter)
        {
            window.Close();
        }
        public void SelectionExecute(object parameter)
        {

        }
        #endregion

        #region IEventHandler
        //public event EventHandler CanExecuteChanged;
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
        private void TestItem()
        {
            //DrinkItems = new DrinkList[]
            //{
            //    new DrinkList
            //    {
            //        Number = "0",
            //        MenuName = "Coding",
            //        Quantity = 4,
            //        Price = 10000,
            //        Option = "It pays the bills",
            //        DrinkItems = new DrinkList[]
            //        {
            //            new DrinkList { MenuName = "Write", Quantity = 2, Option = "C# or go home" },
            //            new DrinkList { MenuName = "Compile", Quantity = 1, Option = "WTB: SSD" },
            //            new DrinkList { MenuName = "Test", Quantity = 1, Option = "Works on my machine" },
            //        },
            //    },
            //    new DrinkList
            //    {
            //        MenuName = "Meetings",
            //        Quantity = 3,
            //        Option = "A necessary evil",
            //        DrinkItems = new DrinkList[]
            //        {
            //            new DrinkList { MenuName = "Boring", Quantity = 1, Option = "Zzzzzz" },
            //            new DrinkList { MenuName = "Gossipy", Quantity = 1, Option = "Oh no he didn't!" },
            //            new DrinkList { MenuName = "Useful", Quantity = 1, Option = "Right away, boss" },
            //        },
            //    },
            //    new DrinkList
            //    {
            //        MenuName = "Communicate",
            //        Quantity = 3,
            //        Option = "No man is an island",
            //        DrinkItems = new DrinkList[]
            //        {
            //            new DrinkList { MenuName = "Email", Quantity = 1, Option = "So much junk mail" },
            //            new DrinkList { MenuName = "Blogs", Quantity = 1, Option = "blogs.msdn.com/delay" },
            //            new DrinkList { MenuName = "Twitter", Quantity = 1, Option = "RT: Nothing to report" },
            //        },
            //    },
            //    new DrinkList
            //    {
            //        MenuName = "Eating",
            //        Quantity = 2,
            //        Option = "Fuel for the body",
            //        DrinkItems = new DrinkList[]
            //        {
            //            new DrinkList { MenuName = "Lunch", Quantity = 1, Option = "Bag lunch from home" },
            //            new DrinkList
            //            {
            //                MenuName = "Snack",
            //                Quantity = 2,
            //                Option = "Still hungry",
            //                DrinkItems = new DrinkList[]
            //                {
            //                    new DrinkList { MenuName = "Fruit", Quantity = 1, Option = "Good for you" },
            //                    new DrinkList { MenuName = "Candy", Quantity = 1, Option = "Yummy!" },
            //                },
            //            },
            //        },
            //    },
            //};
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
            Information = $"Menu : {name},      Price : {price}";
            for (int i = 0; i < 10; i++)
            {
                TB tb;
                TextBlockStore tbStore = new TextBlockStore();
                tb = tbStore.CreateTB("Deco", 500); 
                OptionList.Add(tb);
            }
        }
        private void DrinkAdd(Constants.DrinkType drinkType)
        {
            Drink drink;
            if (drinkType == Constants.DrinkType.Cafucino)
            {
                drink = new Drink();
                GetDrinkInfo(drink);

            }
            else if (drinkType == Constants.DrinkType.Americano)
            {
                drink = new Drink();
                GetDrinkInfo(drink);
            }
            //drink.OrderNo;
        }

        private void GetDrinkInfo(Drink drink)
        {
            drink.OrderNo = "1";
            drink.Name = "Americano";
            drink.Qty = "1";
            drink.Price = 3500;
            drink.Description = "aa";

            Drink.Add(drink);
        }

        public void Sell()
        {

        }

        public void CalculateCharge()
        {

        }

        public void GetOrder()
        {
            string name;
            string price;
            string qty;
            string desc;
        }

        #endregion

        #region IDispose
        #endregion



        public void Execute(object parameter)
        {
            MessageBox.Show("HIHI");
        }
        public void SendMessage(object parameter)
        {
            tcp.SendMessage(SendMsg);
        }
        public event EventHandler CanExecuteChanged;
    }
}
