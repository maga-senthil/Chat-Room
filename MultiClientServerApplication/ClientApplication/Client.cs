using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ClientApplication
{
    class Client
    {
        NetworkStream stream;
        public TcpClient clientSocket;
       
        public Client(string Ip,int port)
        {
            clientSocket = new TcpClient();
            clientSocket.Connect(IPAddress.Parse(Ip), port);
            stream = clientSocket.GetStream();
        }
        public void SendDataToServer()
        {
            while (true)
            {
                string m = Console.ReadLine();
                byte[] message = ASCIIEncoding.ASCII.GetBytes(m);
                stream.Write(message, 0, message.Count());
            }
        }
        public void ReadDataFromServer()
        {
            while (true)
            {
                byte[] message = new byte[256];
                stream.Read(message, 0, message.Length);
                Console.WriteLine(Encoding.ASCII.GetString(message).Trim (new char[] { '\0'}));
            }
        }
    }
}
