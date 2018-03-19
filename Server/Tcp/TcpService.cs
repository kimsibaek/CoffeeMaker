using System;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace CoffeeMaker_Server.Tcp
{
    public class TcpService
    {
        TcpListener server = null;
        TcpClient client = null;
        static int counter = 0;

        public TcpService()
        {
            // socket start
            Thread t = new Thread(InitSocket);
            t.IsBackground = true;
            t.Start();

            Console.ReadKey();
        }
        private void InitSocket()
        {
            server = new TcpListener(IPAddress.Any, 9999);
            client = default(TcpClient);
            server.Start();
            DisplayText(">> Server Started");

            while (true)
            {
                try
                {
                    counter++;
                    client = server.AcceptTcpClient();
                    DisplayText($">> Accept connection from client : {counter}");

                    handleClient h_client = new handleClient();
                    h_client.OnReceived += new handleClient.MessageDisplayHandler(OrderList);
                    h_client.OnCalculated += new handleClient.CalculateClientCounter(CalculateCounter);
                    h_client.startClient(client, counter);
                }
                catch (SocketException se)
                {
                    Trace.WriteLine(string.Format("InitSocket - SocketException : {0}", se.Message));
                }
                catch (Exception ex)
                {
                    Trace.WriteLine(string.Format("InitSocket - Exception : {0}", ex.Message));
                }
            }
        }

        private void CalculateCounter()
        {
            counter--;
        }

        private void DisplayText(string text)
        {
            Console.WriteLine(text);
        }
        private void OrderList(string text)
        {
            Console.WriteLine(text);
        }

        public void TcpClose()
        {
            if (client != null)
            {
                client.Close();
                client = null;
            }

            if (server != null)
            {
                server.Stop();
                server = null;
            }
        }
    }
}
