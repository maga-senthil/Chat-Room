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

        TcpClient client;
        public bool running;
        public string name;
        public bool Running { get; private set; }
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
            NetworkStream stream = client.GetStream();

            CreateClient newclient = new CreateClient(stream, client);
            Console.WriteLine(" >>  Connected");
            
             name = Console.ReadLine();

              byte[] message = Encoding.ASCII.GetBytes(name);
              stream.Write(message, 0, message.Length);
            
              
                while (true)
                {
                    int messageLength = client.Available;
                    if (messageLength > 0)
                    {
                    Console.Write(" >> " + name);
                    newclient.ReadDataFromServer();
                    }
                    if (Console.KeyAvailable)
                    {
                        newclient.SendDataToServer();
                    }
                }
            
        }

        //    Thread thread1 = new Thread(newclient.SendDataToServer);
        //    thread1.Start();
        //    Thread thread2 = new Thread(newclient.ReadDataFromServer);
        //    thread2.Start();
                  
    }
}
