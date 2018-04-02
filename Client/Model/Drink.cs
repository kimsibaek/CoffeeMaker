using CoffeeMaker_Client.Interface;
using CoffeeMaker_Client.TextBlockFactory;
using System.Collections.Generic;
using System.ComponentModel;
using System;

namespace CoffeeMaker_Client.Model
{
    public class Drink : IBeverage
    {
        #region Private Member
        private string _name;
        private int _cost;
        private int _price;
        private string _option;
        private List<IDeco> _optionList;
        #endregion

        #region Property
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        public int Cost
        {
            get { return _cost; }
            set { _cost = value; }
        }
        public int Price
        {
            get { return _price; }
            set { _price = value; }
        }
        public string Option
        {
            get { return _option; }
            set { _option = value; }
        }
        public List<IDeco> OptionList
        {
            get { return _optionList; }
            set { _optionList = value; }
        }
        #endregion
        #region 생성자
        public Drink(string name, int cost)
        {
            _name = name;
            _cost = cost;
            _price = _cost;
            _optionList = new List<IDeco>();
        }
        #endregion

        #region EventHandler
        #endregion

        #region 메서드
        public void AddDeco(DecoTB deco)
        {
            var dc = deco as IDeco;
            if (!_optionList.Contains(dc))
            {
                _optionList.Add(dc);
                _price += deco.Price;
            }
        }
        public void DeleteDeco(DecoTB deco)
        {
            var dc = deco as IDeco;
            if (_optionList.Contains(dc))
            {
                _optionList.Remove(dc);
                _price -= deco.Price;
            }
        }
        public void DeleteDeco(string deco)
        {
            foreach (DecoTB item in _optionList)
            {
                if (item.Menu == deco)
                {
                    var dc = item as IDeco;
                    _optionList.Remove(dc);
                    _price -= item.Price;
                    return;
                } 
            }
        }
        public int GetPrice()
        {
            return Price;
        }

        public void SetOption(string option)
        {
            Option = option;
        }

        public string GetOption()
        {
            return Option;
        }

        public void SetPrice(int price)
        {
            Price = price;
        }
        #endregion
    }
}
