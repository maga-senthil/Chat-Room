using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ServerApplication
{
    class Server
    {
        TcpListener server = null;
        int requestCount;
        public void Initialize()
        {
            try
            {
                Int32 port = 1300;
                IPAddress localAddress = IPAddress.Parse("127.0.0.1");
                server = new TcpListener(localAddress, port);
                server.Start();
                Console.WriteLine("  >> SERVER STARTED.");
                requestCount = 0;
                Byte[] bytes = new Byte[1024];
                Byte[] message = new Byte[1024];
                string dataFromClient = null;
                while (true)
                {
                    requestCount += 1;
                    TcpClient client = server.AcceptTcpClient();
                    Console.WriteLine("  >> Client : {0} Connected" ,Convert.ToString(requestCount));

                    
                        dataFromClient = null;
                        NetworkStream streamData = client.GetStream();
                        dataFromClient = System.Text.Encoding.ASCII.GetString(bytes, 0, streamData.Read(bytes, 0, bytes.Length));
                        Console.WriteLine("Received from Client {1} : {0}", dataFromClient,requestCount);

                        String serverresponse = string.Empty;
                        Console.Write("message: ");
                        serverresponse = Console.ReadLine();
                        message = System.Text.Encoding.ASCII.GetBytes(serverresponse);
                        streamData.Write(message, 0, message.Length);
                        Console.WriteLine("sent :{0}", serverresponse);

                        client.Close();
                    
                }
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException : {0}", e);

            }
            finally
            {
                server.Stop();
            }
            
           //Console.WriteLine("\n Enter to continue...");
         
            }
        
        }
    }


//class ServerApplication
//{
//    TcpListener tcpListener;
//    TcpClient newclient;
//    int clientCount;
//    IPAddress localIpAddress;
//    private Dictionary<string, TcpClient> clientDictionary;
//    string clientData;

//    public static ManualResetEvent allDone = new ManualResetEvent(false);

//    public Dictionary<string, TcpClient> ClientStored
//    {
//        get { return clientDictionary; }
//    }


//    public ServerApplication()
//    {
//        clientDictionary = new Dictionary<string, TcpClient>();

//    }
//    public void StartListener()
//    {
//        try
//        {
//            localIpAddress = IPAddress.Parse("127.0.0.1");
//            tcpListener = new TcpListener(localIpAddress, 1300);
//            newclient = default(TcpClient);
//            clientCount = 0;

//            tcpListener.Start();
//            Console.Title = "Server";
//            Console.WriteLine("  >> SERVER STARTED.");

//            while (true)
//            {
//                clientCount += 1;
//                newclient = tcpListener.AcceptTcpClient();

//                clientData = "client" + Convert.ToString(clientCount);
//                clientDictionary.Add(clientData, newclient);

//                Console.WriteLine("  >> Client : " + Convert.ToString(clientCount));
//                ThreadPool.QueueUserWorkItem(ThreadPick, newclient);

//            }
//        }
//        catch
//        {
//            Console.WriteLine("Connection Failed....");
//        }

//    }



//    public void ThreadPick(object state)
//    {
//        try
//        {
//            var client = (TcpClient)state;
//            Byte[] bytes = new Byte[1024];
//            string dataFromClient = null;

//            try
//            {

//                while (true)
//                {
//                    NetworkStream stream = client.GetStream();
//                    int i = 0;

//                    while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
//                    {
//                        dataFromClient = System.Text.Encoding.ASCII.GetString(bytes, 0, i);
//                        Console.WriteLine("Received: {0}", dataFromClient);

//                        String serverresponse = string.Empty;
//                        Console.Write("message: ");
//                        serverresponse = Console.ReadLine();
//                        Byte[] message = new Byte[1024];
//                        message = System.Text.Encoding.ASCII.GetBytes(serverresponse);
//                        stream.Write(message, 0, message.Length);

//                    }
//                }

//            }
//            catch
//            {
//                Console.WriteLine(">>{0} Lost Connection...", ((IPEndPoint)client.Client.RemoteEndPoint).Address.ToString());
//            }
//            client.Close();

//        }
//        catch (Exception e)
//        {
//            Console.WriteLine(e.ToString());

//        }

//    }

//    private void Broadcast(string message, string userName, bool flag)
//    {
//        foreach (var Item in clientDictionary)
//        {
//            TcpClient serverBroadcast = (TcpClient)Item.Value;
//            NetworkStream broadcastStream = serverBroadcast.GetStream();
//            Byte[] broadcastData = null;

//            if (flag == null)
//            {
//                broadcastData = Encoding.ASCII.GetBytes(userName + ":" + message);
//            }
//            else
//            {
//                broadcastData = Encoding.ASCII.GetBytes(message);
//            }
//            broadcastStream.Write(broadcastData, 0, broadcastData.Length);
//            broadcastStream.Flush();
//        }
//    }
