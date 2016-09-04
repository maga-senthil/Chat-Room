using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ClientApplication
{
    class Program
    {
        static void Main(string[] args)
        {

            ClientClass client = new ClientClass();
            client.ConnectClient();
            
            Console.ReadLine();
           
        }
    }

   
}
