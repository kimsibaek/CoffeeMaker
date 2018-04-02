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

        public void SetDeco(IBeverage beverage)
        {
            this.beverage = beverage;
            this.beverage.SetOption(this.GetDescription());
            this.beverage.SetPrice(beverage.GetPrice() + Price);
        }
        public string GetDescription()
        {
            return beverage.GetOption() + $", {Menu}";
        }
    }
}
