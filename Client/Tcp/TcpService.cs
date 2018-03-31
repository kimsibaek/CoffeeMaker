using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Threading;

namespace CoffeeMaker_Client.TCP
{
    public class TcpService
    {
        TcpClient clientSocket = new TcpClient();
        public TcpService()
        {
            new Thread(delegate ()
            {
                InitSocket();
            }).Start();
        }
        private void InitSocket()
        {
            try
            {
                clientSocket.Connect("172.20.101.237", 9999);
                //  MessageBox.Show("연결");
            }
            catch (SocketException)
            {

            }
            catch (Exception)
            {

            }
        }

        public string SendMessage(string text)
        {
            NetworkStream stream = clientSocket.GetStream();
            byte[] sbuffer = Encoding.Unicode.GetBytes(text + "$");
            stream.Write(sbuffer, 0, sbuffer.Length);
            stream.Flush();

            byte[] rbuffer = new byte[1024];
            stream.Read(rbuffer, 0, rbuffer.Length);
            string msg = Encoding.Unicode.GetString(rbuffer);
            msg = "Data from Server : " + msg;
            return msg;
        }

        public void TcpClose()
        {
            if (clientSocket != null)
                clientSocket.Close();
        }
    }
}
