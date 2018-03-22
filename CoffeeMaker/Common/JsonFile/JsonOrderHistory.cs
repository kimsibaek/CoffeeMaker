using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeMaker.Common.JsonFile
{
    public class JsonOrderHistory
    {
        public AccountItem AccountInfo = new AccountItem();
        public List<OrderHistoryItem> OrderHistoryList = new List<OrderHistoryItem>();
    }
    public class AccountItem
    {
        public string AccountNo;
        public string Receive;
        public string Discount;
        public string Total;
        public string UserNo;
        
    }
    public class OrderHistoryItem
    {
        public string OrderNo;
        public string DrinkNo;
        public string Price;
        public string AccountNo;
        public List<string> DecoList;

    }
}
