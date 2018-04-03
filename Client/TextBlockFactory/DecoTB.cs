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
        public IBeverage beverage;
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

        public void CreateDeco(IBeverage beverage)
        {
            this.beverage = beverage;
            if (this.beverage.SetOption(this.AddOption()))
            {
                this.beverage.SetPrice(beverage.GetPrice() + Price);
            }
        }
        public void DeleteDeco(IBeverage beverage)
        {
            this.beverage = beverage;
            if (this.beverage.SetOption(this.RemoveOption()))
            {
                this.beverage.SetPrice(beverage.GetPrice() - Price);
            }
        }
        public string AddOption()
        {
            return beverage.GetOption() + $" {Menu}";
        }
        public string RemoveOption()
        {
            string[] str = beverage.GetOption().Split(new string[]{ $" {Menu}"}, StringSplitOptions.None);
            string result = "";
            foreach (var item in str)
            {
                result += item;
            }
            return result;
        }
    }
}
