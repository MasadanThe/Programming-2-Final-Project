using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace HOMMCopyCat_Spelprojekt
{
    class NetworkThread
    {
        public void SendData(int gold)
        {
            //Connect
            try
            {
                //Send data
                TcpClient tcpClient = new TcpClient();
                string address = "127.0.0.1";
                int port = 8001;
                tcpClient.Connect(address, port);
                //Convert info to byte and send it
                Byte[] bMessage = Encoding.UTF8.GetBytes("Gold: " + gold);
                Socket socket = tcpClient.Client;
                socket.Send(bMessage);
            }
            catch
            {

            }
        }
    }
}
