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

        public ServerClass ()
        {
            clientDictionary = new Dictionary<string, TcpClient>();
        }
        public TcpClient TcpClient { get { return client; } }

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
                client = tcpListener.AcceptTcpClient();
              
                Thread tcphandlerThread = new Thread(new ThreadStart(tcpHandler));
                tcphandlerThread.Start();
            }

        }

        private void tcpHandler()
        {
            
            stream = client.GetStream();
            Client newClient = new Client(stream, client);
            count += 1;
            string dataFromClient = null;
            Byte[] bytes = new Byte[1024];
            dataFromClient = System.Text.Encoding.ASCII.GetString(bytes, 0, stream.Read(bytes, 0, bytes.Length));
            Console.WriteLine("Client {1} : {0} Connected", dataFromClient, count);

            try
            {
                    clientDictionary.Add(dataFromClient, client);
               
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }
            clientList.Add(newClient);

                 Thread clientdata = new Thread(new ThreadStart(newClient.ReciveData));
                 clientdata.Start();
            foreach (var client in clientDictionary)
            {
              Thread sendData= new Thread (new ThreadStart (newClient.SendData));
                sendData.Start();
            }
           
        }
     
    }
}
