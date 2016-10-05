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
            Client client = new Client("127.0.0.1", 1300);
            Parallel.Invoke(client.ReadDataFromServer, client.SendDataToServer);
        }
    }
}
