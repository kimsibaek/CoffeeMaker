using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeMaker_Client.Interface
{
    public interface IBeverage
    {
        void SetOption(string option);
        string GetOption();
        void SetPrice(int cost);
        int GetPrice();
    }
}
