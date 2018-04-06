using System;
using System.Collections.Generic;
using CoffeeMaker.Common;
using CoffeeMaker.Common.JsonFile;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CoffeeMaker_Server;
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
            decoList.Add("HOT");
            decoList.Add("103");
            decoList.Add("106");
            OrderItem.AccountNo = "100014";
            OrderItem.OrderNo = "10007";
            OrderItem.DrinkNo = "1001";
            OrderItem.Price = "2500";
            OrderItem.DecoList = decoList;
            orderItemListSamples.Add(OrderItem);

            OrderHistoryItem OrderItem2 = new OrderHistoryItem();

            List<string> decoList2 = new List<string>();
            decoList2.Add("HO");
            decoList2.Add("103");
            decoList2.Add("106");
            OrderItem2.AccountNo = "100014";
            OrderItem2.OrderNo = "10008";
            OrderItem2.DrinkNo = "1000";
            OrderItem2.Price = "3000";
            OrderItem2.DecoList = decoList2;
            orderItemListSamples.Add(OrderItem2);

            OrderHisorySample.OrderHistoryList = orderItemListSamples;

            OrderHistoryItem OrderItem3 = new OrderHistoryItem();

            List<string> decoList3 = new List<string>();
            decoList3.Add("HOT");
            decoList3.Add("103");
            decoList3.Add("106");
            OrderItem3.AccountNo = "100014";
            OrderItem3.OrderNo = "10009";
            OrderItem3.DrinkNo = "1001";
            OrderItem3.Price = "3000";
            OrderItem3.DecoList = decoList3;
            orderItemListSamples.Add(OrderItem3);

            OrderHisorySample.OrderHistoryList = orderItemListSamples;

            //Account Samples
            AccountSample.AccountNo = "100014";
            AccountSample.Discount = "0";
            AccountSample.Receive = "3000";
            AccountSample.Total = "3000";
            
            AccountSample.UserNo = "0";

            OrderHisorySample.AccountInfo = AccountSample;

            string sampleStr=JsonExtention.Serialize<JsonOrderHistory>(OrderHisorySample);
            var serverListener = new OrderListener();
            
            serverListener.GetMessage(sampleStr);
        }
    }
}
