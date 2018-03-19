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


namespace CoffeeMaker_Client.ViewModel
{
    public class CoffeeMakerViewModel

    {
        #region Private Members

        private RelayCommand _drinkCommand;
        private string _sendMsg;
        TcpService tcp = null;
        #endregion

        #region Properties
        public string SendMsg
        {
            get
            {
                return _sendMsg;
            }
            set
            {
                _sendMsg = value;
            }
        }

        private ObservableCollection<Drink> _drink;

        public ObservableCollection<Drink> Drink
        {
            get { return _drink; }
            set { _drink = value; }
        }

        #endregion

        #region 생성자

        public CoffeeMakerViewModel(/*MainWindow view*/)
        {
            //_view = view;
            tcp = new TcpService();
            Database.Database database = new Database.Database();
            Drink = new ObservableCollection<Drink>();
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
        #endregion

        #region IEventHandler
        //public event EventHandler CanExecuteChanged;
        #endregion

        #region EventMethods
        #endregion

        #region Methods


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
            drink.Price = "3500원";
            drink.Description = "aa";

            Drink.Add(drink);

            //_view.ListViewCoffee.ItemsSource = Drink;
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
