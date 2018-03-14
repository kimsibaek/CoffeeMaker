using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using CoffeMakcer.Model;


namespace CoffeMakcer.ViewModel
{
    class CoffeeMaker_Vmd :ICommand
    {
        #region Private Members
        Drink cafucino = new Drink();
        #endregion
           
        #region Properties

        
        
        #endregion

        #region 생성자

        public CoffeeMaker_Vmd()
        {
            
        }
        #endregion

        #region Command



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
