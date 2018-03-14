using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using CoffeMakcer.Model;
using CoffeMaker.ViewModel;


namespace CoffeMakcer.ViewModel
{
    class CoffeeMaker_Vmd :ICommand
    {
        #region Private Members

        private RelayCommand _drinkCommand;
        #endregion
           
        #region Properties
        
        
        #endregion

        #region 생성자

        public CoffeeMaker_Vmd()
        {
            
        }
        #endregion

        #region Command

        public ICommand Cafucino
        {
            get
            {
                if (_drinkCommand == null)
                {
                    _drinkCommand = new RelayCommand(Execute,CanExecute);
                }
                return _drinkCommand;
            }
        }
        #endregion

        #region IEventHandler
        #endregion

        #region EventMethods
        #endregion

        #region Methods
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

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            MessageBox.Show("HIHI");
        }

        public event EventHandler CanExecuteChanged;
    }
}
