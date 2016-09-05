using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace ServerApplication
{
    public class Client
    {
       private  Queue clientMessageQueue = new Queue();

        NetworkStream stream;
        public TcpClient client;
        public string name;
        Byte[] bytes = new Byte[1024];
        Byte[] msg = new Byte[1024];
        public String data;
        byte[] message = new byte[1204];

        public Client (NetworkStream stream,TcpClient client)
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
            if (data != null)
            {

                if (clientMessageQueue != null)
                {
                    msg = System.Text.Encoding.ASCII.GetBytes(data);
                   stream.Write(msg, 0, msg.Length);
                 }
                //if (clientMessageQueue != null)
                //{
                //    clientMessageQueue.Dequeue();
                //}
            }
        }
    }
}


  
    

    
