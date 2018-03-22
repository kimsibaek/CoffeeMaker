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
    public class CoffeeMakerViewModel

    {
        #region Private Members

        private RelayCommand _drinkCommand;
        private RelayCommand _OKCommand;
        private RelayCommand _CancelCommand;
        private ObservableCollection<Drink> _drink;
        private ObservableCollection<Btn> _buttonList;
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

        public ObservableCollection<TextBlock> OptionList
        {
            get { return _optionList; }
            set { _optionList = value; }
        }
        #endregion

        #region 생성자

        public CoffeeMakerViewModel()
        {
            tcp = new TcpService();
            Database.Database database = new Database.Database();
            Drink = new ObservableCollection<Drink>();
            ButtonList = new ObservableCollection<Btn>();
            TestCode();
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

        private void OnClick(object sender, RoutedEventArgs e)
        {
            //SubBtn item = sender as SubBtn;
            //Drink.Add(new Model.Drink() { Name = item.Name, Price = item.Price, OrderNo = Drink.Count.ToString() } );

            Window window = new Window
            {
                Title = "",
                Content = new View.DecoView(this)
            };
            window.Height = 600;
            window.Width = 500;
            window.ShowDialog();
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
