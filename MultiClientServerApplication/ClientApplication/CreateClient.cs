using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ClientApplication
{
    class CreateClient
    {
        NetworkStream stream;
        public TcpClient client;
        Byte[] data = new Byte[1024];
        Byte[] msg = new Byte[1024];

        public CreateClient(NetworkStream stream, TcpClient client)
        {
            this.stream = stream;
            this.client = client;
        }

        public void SendDataToServer()
        {
                string m = Console .ReadLine();
                byte[] message =ASCIIEncoding .ASCII.GetBytes(m);
                stream.Write(message, 0, message.Length);

        }
        public void ReadDataFromServer()
        {
            byte[] message = new byte[1204];
            Int64 i = stream.Read(message, 0, message.Length);
            Console.WriteLine(Encoding.ASCII.GetString(message));
           
        }
    }
}
