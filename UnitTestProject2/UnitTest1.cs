using System;
using System.Collections.Generic;
using CoffeeMaker.Common;
using CoffeeMaker.Common.JsonFile;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CoffeeMaker_Server.Maker;

namespace UnitTestProject2
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void WhenGetValueFromCleint()
        {
            
            JsonOrderHistory OrderHisorySample = new JsonOrderHistory();

            List<OrderHistoryItem> orderItemListSamples = new List<OrderHistoryItem>();
            AccountItem AccountSample = new AccountItem();

            //OrderHistory Samples
            OrderHistoryItem OrderItem = new OrderHistoryItem();

            List<string> decoList = new List<string>();
            decoList.Add("101");
            decoList.Add("103");
            decoList.Add("106");
            OrderItem.AccountNo = "100000";
            OrderItem.OrderNo = "10000";
            OrderItem.DrinkNo = "1001";
            OrderItem.Price = "2500";
            OrderItem.DecoList = decoList;
            orderItemListSamples.Add(OrderItem);

            OrderHistoryItem OrderItem2 = new OrderHistoryItem();

            List<string> decoList2 = new List<string>();
            decoList2.Add("102");
            decoList2.Add("104");
            decoList2.Add("106");
            OrderItem2.AccountNo = "100000";
            OrderItem2.OrderNo = "10001";
            OrderItem2.DrinkNo = "1002";
            OrderItem2.Price = "2500";
            OrderItem2.DecoList = decoList2;
            orderItemListSamples.Add(OrderItem2);

            OrderHisorySample.OrderHistoryList = orderItemListSamples;

            //Account Samples
            AccountSample.AccountNo = "100000";
            AccountSample.Discount = "0";
            AccountSample.Receive = "7500";
            AccountSample.Total = "7500";
            
            AccountSample.UserNo = "4";

            OrderHisorySample.AccountInfo = AccountSample;

            string sampleStr=JsonExtention.Serialize<JsonOrderHistory>(OrderHisorySample);

            var serverListener = new OrderListener(sampleStr);
        }
    }
}
