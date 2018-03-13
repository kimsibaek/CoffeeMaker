using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeMakcer.Model
{
    class Drink
    {
        private string _name;
        private string _qty;
        private string _price;
        private string _descriotn;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public string Qty
        {
            get { return _qty; }
            set { _qty = value; }
        }

        public string Price
        {
            get { return _price; }
            set { _price = value; }
        }

        public string Description
        {
            get { return _descriotn; }
            set { _descriotn = value; }
        }
    }
}
