using CoffeeMaker_Client.Binding;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeMaker_Client.Model
{
    public class DrinkList : BaseViewModel
    {
        private string _number;
        private string _menuName;
        private int _quantity;
        private int _price;
        private string _option;
        private ObservableCollection<DrinkList> _drinkItems;

        public string Number
        {
            get { return _number; }
            set
            {
                _number = value;
                OnPropertyChanged(nameof(Number));
            }
        }
        public string MenuName
        {
            get { return _menuName; }
            set
            {
                _menuName = value;
                OnPropertyChanged(nameof(MenuName));
            }
        }
        public int Quantity
        {
            get { return _quantity; }
            set
            {
                _quantity = value;
                OnPropertyChanged(nameof(Quantity));
            }
        }
        public int Price
        {
            get { return _price; }
            set
            {
                _price = value;
                OnPropertyChanged(nameof(Price));
            }
        }
        public string Option
        {
            get { return _option; }
            set
            {
                _option = value;
                OnPropertyChanged(nameof(Option));
            }
        }
        public ObservableCollection<DrinkList> DrinkItems
        {
            get { return _drinkItems; }
            set
            {
                _drinkItems = value;
                OnPropertyChanged(nameof(DrinkItems));
            }
        }
        public DrinkList()
        {
            DrinkItems = new ObservableCollection<DrinkList>();
        }
    }
}
