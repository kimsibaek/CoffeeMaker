using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeMaker_Client.TextBlockFactory
{
    public class TextBlockStore
    {
        public TB CreateTB( string name, int price = 0)
        {
            TB btn = null;
            btn = new MainTB(name, price);
            return btn;
        }
    }
}
