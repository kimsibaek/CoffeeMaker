using CoffeeMaker_Client.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace CoffeeMaker_Client.TextBlockFactory
{
    public class DecoTB : TB, IDeco
    {
        #region 필드
        public IBeverage beverage;
        #endregion

        #region 생성자
        public DecoTB(string menu, int price)
        {
            Height = 40;
            Width = 80;
            Foreground = new SolidColorBrush() { Color = Colors.White };
            FontSize = 20;
            TextAlignment = System.Windows.TextAlignment.Center;

            Text = menu;
            Menu = menu;
            Price = price;
        }
        #endregion

        #region 메서드
        public string AddOption(IBeverage beverage)
        {
            return beverage.GetOption() + $" {Menu}";
        }

        public string RemoveOption(IBeverage beverage)
        {
            string[] str = beverage.GetOption().Split(new string[]{ $" {Menu}"}, StringSplitOptions.None);
            string result = "";
            foreach (var item in str)
            {
                result += item;
            }
            return result;
        }

        public void CreateDeco(IBeverage beverage)
        {
            this.beverage = beverage;
            if (this.beverage.SetOption(this.AddOption(beverage)))
            {
                this.beverage.SetPrice(beverage.GetPrice() + Price);
            }
        }

        public void DeleteDeco(IBeverage beverage)
        {
            this.beverage = beverage;
            if (this.beverage.SetOption(this.RemoveOption(beverage)))
            {
                this.beverage.SetPrice(beverage.GetPrice() - Price);
            }
        }
        #endregion
    }
}
