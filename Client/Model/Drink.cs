using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoffeeMaker_Client.Model;

namespace CoffeeMaker_Client.Model
{
    public class Drink
    {
        #region Private Member
        private string _orderNo;
        private string _name;
        private string _qty;
        private string _price;
        private string _description;
        #endregion

        #region Property
        public string OrderNo
        {
            get { return _orderNo; }
            set
            {
                _orderNo = value;
                OnPropertyChanged(nameof(OrderNo));
            }
        }

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public string Qty
        {
            get { return _qty; }
            set
            {
                _qty = value;
                OnPropertyChanged(nameof(Qty));
            }
        }

        public string Price
        {
            get { return _price; }
            set
            {
                _price = value;
                OnPropertyChanged(nameof(Price));
            }
        }

        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                OnPropertyChanged(nameof(Description));
            }
        }
        #endregion

        #region EventHandler
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region EventCall
        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }

    //class Americano : Drink
    //{

    //}

    //class Cafelatte : Drink
    //{

    //}
}
