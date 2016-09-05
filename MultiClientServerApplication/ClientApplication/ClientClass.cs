using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Net;

namespace ClientApplication
{
    class ClientClass
    {

        NetworkStream stream;
        TcpClient client;
        public void ConnectClient()
        {
            Thread mthread = new Thread(new ThreadStart(ConnecttcpClient));
            mthread.Start();
        }

        private void ConnecttcpClient()
        {
             client = new TcpClient();

            client.Connect("127.0.0.1", 1300);
            Console.Title = "CLIENT";
            Console.WriteLine(" >>  Connected");


            NetworkStream stream = client.GetStream();

            CreateClient newclient = new CreateClient(stream, client);

            NetworkStream streamName = client.GetStream();
            Console.WriteLine("Enter your name");
            string s = Console.ReadLine();

            byte[] message = Encoding.ASCII.GetBytes(s);
            streamName.Write(message, 0, message.Length);


            Thread thread1 = new Thread(newclient.SendDataToServer);
            thread1.Start();
            Thread thread2 = new Thread(newclient.ReadDataFromServer);
            thread2.Start();
            //Console.Write("Enter your name: ");
            //string s = Console .ReadLine();

            //byte[] message = Encoding.ASCII.GetBytes(s);
            //stream.Write(message, 0, message.Length);
            //Console.WriteLine("message sent");
            //stream.Close();
            //client.Close();
        }  
    }
}
