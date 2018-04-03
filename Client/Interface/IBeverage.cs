using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeMaker_Client.Interface
{
    public interface IBeverage
    {
        bool SetOption(string option);
        string GetOption();
        void SetPrice(int price);
        int GetPrice();
    }
}
