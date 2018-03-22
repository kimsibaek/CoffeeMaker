using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoffeeMaker.Common;
using CoffeeMaker.Common.JsonFile;
using CoffeeMaker_Server.Database;

namespace CoffeeMaker_Server.Maker
{
    public class OrderListener
    {
        private readonly List<OrderHistoryItem> _orderHistoryList;
        private readonly AccountItem _accountInfo;
        private readonly DbQuery _query = new DbQuery();

        public OrderListener(string tcpStr)
        {
            var orderInfo = JsonExtention.Deserialize<JsonOrderHistory>(tcpStr);
            _orderHistoryList = orderInfo.OrderHistoryList;
            _accountInfo = orderInfo.AccountInfo;
            DataProcess();
        }

        private void DataProcess()
        {
            Create_OrderHistoryData();
            Create_AccountData();
        }
        private void Create_OrderHistoryData()
        {
            int result = 0;
            DataTable dtOrderHistory = new DataTable();

            dtOrderHistory.Columns.Add("ODERNO", typeof(string));
            dtOrderHistory.Columns.Add("DRINKNO", typeof(string));
            dtOrderHistory.Columns.Add("DRINKPRICE", typeof(string));
            dtOrderHistory.Columns.Add("HOTICE", typeof(string));
            dtOrderHistory.Columns.Add("SIZE", typeof(string));
            dtOrderHistory.Columns.Add("SHOT", typeof(string));
            dtOrderHistory.Columns.Add("ACCOUNTNO", typeof(string));

            foreach (var orderHistoryItem in _orderHistoryList)
            {
                DataRow drOrderHisytory = dtOrderHistory.NewRow();
                drOrderHisytory["ODERNO"] = orderHistoryItem.OrderNo;
                drOrderHisytory["DRINKNO"] = orderHistoryItem.DrinkNo;
                drOrderHisytory["ACCOUNTNO"] = orderHistoryItem.AccountNo;
                drOrderHisytory["DRINKPRICE"] = orderHistoryItem.Price;
                drOrderHisytory["HOTICE"] = orderHistoryItem.DecoList[0];
                drOrderHisytory["SIZE"] = orderHistoryItem.DecoList[1];
                drOrderHisytory["SHOT"] = orderHistoryItem.DecoList[2];
                dtOrderHistory.Rows.Add(drOrderHisytory);
            }
            //dtOrderHistory
            result= _query.DML_InsertOrderHistory(dtOrderHistory);

            if (result < 0)
            {
                Console.Write("Order History Error");
            }

        }
        private void Create_AccountData()
        {
            int result = 0;
            DataTable dtAccount = new DataTable();

            dtAccount.Columns.Add("ACCOUNTNO", typeof(string));
            dtAccount.Columns.Add("RECEIVE", typeof(string));
            dtAccount.Columns.Add("DISCOUNT", typeof(string));
            dtAccount.Columns.Add("TOTAL", typeof(string));
            dtAccount.Columns.Add("USERNO", typeof(string));
            
            DataRow drAccount= dtAccount.NewRow();
            drAccount["ACCOUNTNO"] = _accountInfo.AccountNo;
            drAccount["RECEIVE"] = _accountInfo.Receive;
            drAccount["DISCOUNT"] = _accountInfo.Discount;
            drAccount["TOTAL"] = _accountInfo.Total;
            drAccount["USERNO"] = _accountInfo.UserNo;

            result = _query.DML_InsertAccount(dtAccount);

            if (result < 0)
            {
                Console.Write("Account Error");
            }
        }
    }
}
