using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using CoffeeMaker_Client;
using CoffeeMaker_Client.Model;
using CoffeeMaker_Client.ViewModel;
using static CoffeeMaker_Client.ViewModel.Constants;


namespace CoffeeMaker_Client.ViewModel
{
    class CoffeeMaker_Vmd 
    {
        #region Private Members

        private RelayCommand _drinkCommand;
        #endregion

        #region Properties

        public List<Drink> Drink { get; } = new List<Drink>();

        #endregion

        #region 생성자

        public CoffeeMaker_Vmd()
        {
            Database database = new Database();
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
        #endregion

        #region Command Method
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void CafucinoExecute(object parameter)
        {
            DrinkAdd(DrinkType.Cafucino);
        }

        public void AmericanoExecute(object parameter)
        {
            DrinkAdd(DrinkType.Americano);
        }
        #endregion

        #region IEventHandler
        public event EventHandler CanExecuteChanged;
        #endregion

        #region EventMethods
        #endregion

        #region Methods
        

        private void DrinkAdd(DrinkType drinkType)
        {
            Drink drink;
            if (drinkType == DrinkType.Cafucino)
            {
                drink = new Drink();
                GetDrinkInfo(drink);
               
            }
            else if (drinkType == DrinkType.Americano)
            {
                drink = new Drink();
                GetDrinkInfo(drink);
            }
            //drink.OrderNo;
        }

        private void GetDrinkInfo(Drink drink)
        {
            drink.OrderNo = "1";
            drink.Description = "aa";
            drink.Qty = "1";
            drink.Price = "3500원";
            drink.Name = "Cafucino";
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

       

        
    }
}
