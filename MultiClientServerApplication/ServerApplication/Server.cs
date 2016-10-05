using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Net;
using System.Collections.Concurrent;

namespace ServerApplication
{
    class Server
    {
        public static ConcurrentQueue<Message> messageQueue = new ConcurrentQueue<Message>();
        TcpListener tcpListener;
        IPAddress localIpAddress;
        List<Client> clientList = new List<Client>();
       
        public Server()
        {
            localIpAddress = IPAddress.Parse("127.0.0.1");
            tcpListener = new TcpListener(localIpAddress , 1300);
            tcpListener.Start();
        }
        public void ServerStart()
        {
            Console.Title = "SERVER";
            Console.WriteLine(" >> Server started.");
            Parallel.Invoke(ConnectToClient, SendToAll);
            Thread.Sleep(10);
        }
        public void ConnectToClient()
        {
            while (true)
            {
                TcpClient tcpClient = default(TcpClient);
                tcpClient = tcpListener.AcceptTcpClient();
                Console.WriteLine("Connected");
                NetworkStream stream = tcpClient.GetStream();
                Client client = new Client(stream, tcpClient);
                clientList.Add(client);
                Thread tcphandlerThread = new Thread(new ThreadStart(client.ReciveData));
                tcphandlerThread.Start();
            }
        }
        private void SendToAll()
        {
            while (true)
            {
                Message message = new Message(null, "");

                if (messageQueue.TryDequeue(out message))
                {
                    foreach (Client client in clientList)
                  
                    {
                        if (client !=message .messageSender )
                        {
                            client.SendData(message .bodyOfMessage);
                        }
                    }
                }
            }
        }
    }
}
