using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace CoffeeMaker_Client.TextBlockFactory
{
    public abstract class TB : TextBlock
    {
        public string Menu { get; set; }
        public int Price { get; set; }
        public TB()
        {

        }
        
    }
}
