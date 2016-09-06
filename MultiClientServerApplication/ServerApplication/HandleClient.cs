using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace ServerApplication
{
    public class HandleClient
    {
       private  Queue<string> clientMessageQueue = new Queue<string>();

        NetworkStream stream;
        public TcpClient client;
        public string name;
        Byte[] bytes = new Byte[1024];
        Byte[] msg = new Byte[1024];
        public String data;
        byte[] message = new byte[1204];

        public HandleClient (NetworkStream stream,TcpClient client)
        {
            this.stream = stream;
            this.client = client;
        }
      
        public void ReciveData()
        {
           
            int i = stream.Read(message, 0, message.Length);
            data = Encoding.ASCII.GetString(message);

           clientMessageQueue.Enqueue(data);
           
        }

        public void SendData()
        {
                foreach (string msgOut in clientMessageQueue)
                {
                    msg = System.Text.Encoding.ASCII.GetBytes(msgOut);
                    stream.Write(msg, 0, msg.Length);
                }
            clientMessageQueue.Clear();
        }
    }
}


  
    

    
