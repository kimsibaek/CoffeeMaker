using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeMaker_Client.Model
{
    public class DrinkList
    {
        public string Number { get; set; }
        public string MenuName { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
        public string Option { get; set; }
        public DrinkList[] DrinkItems { get; set; }
        public DrinkList()
        {
            DrinkItems = new DrinkList[0];
        }
    }
}
