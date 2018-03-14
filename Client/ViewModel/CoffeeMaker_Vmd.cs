using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using CoffeMakcer.Model;
using CoffeMaker.ViewModel;
using CoffeMakcer.Tcp;

namespace CoffeMakcer.ViewModel
{
    class CoffeeMaker_Vmd :ICommand
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

        #endregion

        #region 생성자

        public CoffeeMaker_Vmd()
        {
            tcp = new TcpService();
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

        public ICommand SendMessageCommand
        {
            get
            {
                return new RelayCommand(SendMessage);
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
        public void SendMessage(object parameter)
        {
            tcp.SendMessage(SendMsg);
        }
        public event EventHandler CanExecuteChanged;
    }
}
