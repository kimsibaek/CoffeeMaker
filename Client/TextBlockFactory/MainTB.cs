using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace CoffeeMaker_Client.TextBlockFactory
{
    public class MainTB : TB
    {
        public int price { get; set; }
        public MainTB(string name, int price)
        {
            Height = 40;
            Width = 80;
            Foreground = new SolidColorBrush() { Color = Colors.White };
            FontSize = 20;
            TextAlignment = System.Windows.TextAlignment.Center;

            Text = name;
            this.price = price;
        }
    }
}
