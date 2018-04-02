using CoffeeMaker_Client.TextBlockFactory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeMaker_Client.TextBlockFactory
{
    public enum TBType
    { 
        Main,
        Deco
    }
    public class TextBlockStore
    {
        public void CreateTB(out TB tb, TBType type, string name, int price = 0)
        {
            if (type == TBType.Main) tb = new MainTB(name);
            else if (type == TBType.Deco) tb = new DecoTB(name, price);
            else tb = new TB();
        }
    }
}
