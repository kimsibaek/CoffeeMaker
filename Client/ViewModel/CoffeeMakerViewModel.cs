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

namespace CoffeeMaker_Client.ViewModel
{
    public class ScheduleItem
    {
        public string Task { get; set; }
        public double Duration { get; set; }
        public string Notes { get; set; }
        public ScheduleItem[] SubItems { get; set; }
        public ScheduleItem()
        {
            SubItems = new ScheduleItem[0];
        }
    }
    public class CoffeeMakerViewModel

    {
        #region Private Members
        private Window window;
        private RelayCommand _drinkCommand;
        private RelayCommand _OKCommand;
        private RelayCommand _CancelCommand;
        private ScheduleItem[] _subItems;
        private ObservableCollection<Drink> _drink;
        private ObservableCollection<Btn> _buttonList;
        private int _temperatureSelectIdx;
        private int _sizeSelectIdx;
        private ObservableCollection<TextBlock> _optionList;
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
        public ObservableCollection<TextBlock> OptionList
        {
            get { return _optionList; }
            set { _optionList = value; }
        }
        public ScheduleItem[] SubItems
        {
            get { return _subItems; }
            set { _subItems = value; }
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
            OptionList = new ObservableCollection<TextBlock>();
            TestCode();
            TestItem();
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

        }
        public void CancelExecute(object parameter)
        {
            window.Close();
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
            SubItems = new ScheduleItem[]
            {
                new ScheduleItem
                {
                    Task = "Coding",
                    Duration = 4,
                    Notes = "It pays the bills",
                    SubItems = new ScheduleItem[]
                    {
                        new ScheduleItem { Task = "Write", Duration = 2, Notes = "C# or go home" },
                        new ScheduleItem { Task = "Compile", Duration = 1, Notes = "WTB: SSD" },
                        new ScheduleItem { Task = "Test", Duration = 1, Notes = "Works on my machine" },
                    },
                },
                new ScheduleItem
                {
                    Task = "Meetings",
                    Duration = 2,
                    Notes = "A necessary evil",
                    SubItems = new ScheduleItem[]
                    {
                        new ScheduleItem { Task = "Boring", Duration = 1, Notes = "Zzzzzz" },
                        new ScheduleItem { Task = "Gossipy", Duration = 0.75, Notes = "Oh no he didn't!" },
                        new ScheduleItem { Task = "Useful", Duration = 0.25, Notes = "Right away, boss" },
                    },
                },
                new ScheduleItem
                {
                    Task = "Communicate",
                    Duration = 1,
                    Notes = "No man is an island",
                    SubItems = new ScheduleItem[]
                    {
                        new ScheduleItem { Task = "Email", Duration = 0.5, Notes = "So much junk mail" },
                        new ScheduleItem { Task = "Blogs", Duration = 0.25, Notes = "blogs.msdn.com/delay" },
                        new ScheduleItem { Task = "Twitter", Duration = 0.25, Notes = "RT: Nothing to report" },
                    },
                },
                new ScheduleItem
                {
                    Task = "Eating",
                    Duration = 1.5,
                    Notes = "Fuel for the body",
                    SubItems = new ScheduleItem[]
                    {
                        new ScheduleItem { Task = "Lunch", Duration = 1, Notes = "Bag lunch from home" },
                        new ScheduleItem
                        {
                            Task = "Snack",
                            Duration = 0.5,
                            Notes = "Still hungry",
                            SubItems = new ScheduleItem[]
                            {
                                new ScheduleItem { Task = "Fruit", Duration = 0.25, Notes = "Good for you" },
                                new ScheduleItem { Task = "Candy", Duration = 0.25, Notes = "Yummy!" },
                            },
                        },
                    },
                },
            };
        }
        private void OnClick(object sender, RoutedEventArgs e)
        {
            //SubBtn item = sender as SubBtn;
            //Drink.Add(new Model.Drink() { Name = item.Name, Price = item.Price, OrderNo = Drink.Count.ToString() } );

            window = new Window
            {
                Title = "",
                Content = new View.DecoView(this)
            };
            window.Height = 600;
            window.Width = 500;
            AddOptionItems();
            window.ShowDialog();
        }
        private void AddOptionItems()
        {
            for (int i = 0; i < 10; i++)
            {
                TextBlock textBlock = new TextBlock() { Text = "test", Height = 40, Width = 80, Foreground = new SolidColorBrush() { Color = Colors.White }, FontSize = 20, TextAlignment = TextAlignment.Center };
                OptionList.Add(textBlock);
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
