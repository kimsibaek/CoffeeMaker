using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeMaker_Client.ButtonFactory
{
    class SubBtn : Btn
    {
        public int Price { get; set; }
        public SubBtn(string name, int price)
        {
            Content = name;
            Price = price;

            Height = 100;
            Width = 200;
        }
    }
}
