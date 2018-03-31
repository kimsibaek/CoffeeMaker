using CoffeeMaker_Client.TextBlockFactory;
using System.Collections.Generic;
using System.ComponentModel;


namespace CoffeeMaker_Client.Model
{
    public class Drink
    {
        #region Private Member
        private string _name;
        private int _cost;
        private int _price;
        private string _option;
        private List<TB> _optionList;
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
        public List<TB> OptionList
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
            _optionList = new List<TB>();
        }
        #endregion

        #region EventHandler
        #endregion

        #region 메서드
        public void AddDeco(TB deco)
        {
            if (!_optionList.Contains(deco))
            {
                _optionList.Add(deco);
                _price += deco.Price;
            }
        }
        public void DeleteDeco(TB deco)
        {
            if (_optionList.Contains(deco))
            {
                _optionList.Remove(deco);
                _price -= deco.Price;
            }
        }
        public void DeleteDeco(string deco)
        {
            foreach (TB item in _optionList)
            {
                if (item.Name == deco)
                {
                    _optionList.Remove(item);
                    _price -= item.Price;
                    return;
                } 
            }
        }
        public int GetCost()
        {
            return Price;
        }
        #endregion
    }
}
