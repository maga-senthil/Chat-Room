using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace ServerApplication
{
    public class Client
    {
        NetworkStream stream;
        public TcpClient client;
        public string name;
        public string userId;

        public Dictionary<string, TcpClient> clientDictionary = new Dictionary<string, TcpClient>();

        public Dictionary<string, TcpClient> ClientStored
        {
            get { return clientDictionary; }
        }

        public Client (NetworkStream stream,TcpClient client)
        {
            this.stream = stream;
            this.client = client;
            userId = Guid.NewGuid().ToString();
        }
        public void GetUsername()
        {
            SendData("Welcome to chatroom:\nEnter your name:");
            byte[] receivedMessage = new byte[256];
            stream.Read(receivedMessage, 0, receivedMessage.Length);
            name = Encoding.ASCII.GetString(receivedMessage).Trim(new char[] { '\0' });
        }
        public void SendData(string message)
        {
            byte[] msg = Encoding.ASCII.GetBytes(message);
            stream.Write(msg, 0, msg.Count());
        }
        public void ReciveData()
        {
            GetUsername();
            clientDictionary.Add(name, client);
            while (true)
            {
                try
                {
                    byte[] receivedMessage = new byte[256];
                    stream.Read(receivedMessage, 0, receivedMessage.Length);
                    String receievedMessageString = Encoding.ASCII.GetString(receivedMessage).Trim(new char[] { '\0' });
                    receievedMessageString = receievedMessageString.Insert(0, name + ": ");
                    Message message = new Message(this, receievedMessageString);
                    Server.messageQueue.Enqueue(message);
                }
                catch 
                {
                    Console.WriteLine(" client Disconnected");
                    break;
                }
            }
        }
    }
}


  
    

    
