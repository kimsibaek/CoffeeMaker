using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeMaker_Client.Interface
{
    public interface IDeco
    {
        void CreateDeco(IBeverage beverage);
        void DeleteDeco(IBeverage beverage);
    }
}
