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
        List<TcpClient> clientList = new List<TcpClient>();
        private Dictionary<string, TcpClient> clientDictionary;
        public readonly int BufferSize = 2 * 1024;
        byte[] message = new byte[1204];
        string data;
        TcpClient client;
        int count =0;

        public ServerClass ()
        {
            clientDictionary = new Dictionary<string, TcpClient>();
        }
        public bool Running { get; private set; }
      

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
            Console.WriteLine(" >> Server started.");
            Running = true;
            while (Running)
            {
                client = tcpListener.AcceptTcpClient();
              
                Thread tcphandlerThread = new Thread(new ThreadStart(tcpHandler));
                tcphandlerThread.Start();
            }

        }

        private void tcpHandler()
        {

            NetworkStream stream = client.GetStream();
            HandleClient newClient = new HandleClient(stream, client);

            Byte[] bytes = new Byte[BufferSize];
            int bytesRead = stream.Read(bytes, 0, bytes.Length);

                string dataFromClient = System.Text.Encoding.ASCII.GetString(bytes, 0, bytesRead);
                Console.WriteLine(" >> {0} Connected", dataFromClient);

                clientDictionary.Add(dataFromClient, client);

                clientList.Add(client);

                foreach (TcpClient R in clientList)
                {
                     newClient.ReciveData();
                }

                foreach (TcpClient s in clientList)
                {
                    newClient.SendData();
                }

            
        }

      
            //Thread clientdata = new Thread(new ThreadStart(newClient.ReciveData));
            //clientdata.Start();
            //foreach (var client in clientDictionary)
            //{
            //  Thread sendData= new Thread (new ThreadStart (newClient.SendData));
            //    sendData.Start();
            //}
           
        
     
    }
}
