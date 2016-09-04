using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Net;

namespace ServerApplication
{
    class ServerClass
    {
        TcpListener tcpListener;
        TcpClient client;
        List<Client> clientList = new List<Client>();
        private Dictionary<string, TcpClient> clientDictionary;
        byte[] message = new byte[1204];
        string data;
        NetworkStream stream;
        int count =0;

        public Dictionary<string, TcpClient> ClientStored
        {
            get { return clientDictionary; }
        }

        public void ServerStart()
        {
            Thread tcpServerRunThread = new Thread(new ThreadStart(TcpServerRun));
            tcpServerRunThread.Start();
        }
        public void TcpServerRun()
        {
            tcpListener = new TcpListener(IPAddress.Any, 1300);
            tcpListener.Start();
            Console.Title = "SERVER";
            Console.WriteLine("Listening");

            while (true)
            {
                count += 1;
                client = tcpListener.AcceptTcpClient();
                Console.WriteLine("Connected");
              
                Thread tcphandlerThread = new Thread(new ParameterizedThreadStart (tcpHandler));
                tcphandlerThread.Start(client);
            }
        }

        private void tcpHandler(object clientnew)
        {
            
            stream = client.GetStream();
            Client newClient = new Client(stream, client);
            clientList.Add(newClient);

            while (true)
            {
                Thread clientdata = new Thread(new ThreadStart(newClient.ReciveData));
                clientdata.Start();
              
                if (clientList.Count  > 0)
                {
                    SendToAll(clientList);
                }
                else
                {
                    newClient.SendData();
                }
                             
            }
        }
        public void SendToAll(List<Client> clientList)
        {

            foreach (Client client in clientList)
            {
                client.SendData();
            }

        }

    }
}
