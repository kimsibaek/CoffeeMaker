using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeMaker_Client.ButtonFactory
{
    public enum BtnType
    {
        Main,
        Sub
    }
    public class ButtonStore
    {
        public Btn CreateBtn(BtnType type, string name, int price = 0)
        {
            Btn btn = null;
            if (type == BtnType.Main) btn = new MainBtn(name);
            else if(type == BtnType.Sub) btn = new SubBtn(name, price);
            return btn;
        }
    }
}
