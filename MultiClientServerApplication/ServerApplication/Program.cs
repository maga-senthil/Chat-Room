using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerApplication
{
    class Program
    {
        static void Main(string[] args)
        {

            ServerClass server = new ServerClass();
            server.ServerStart();
           
            ////Parallel.Invoke(server.SendToClient, server.ReceiveData);
            Console.ReadLine();
        }

        internal static void Broadcast(string clientData, string clId, bool v)
        {
            throw new NotImplementedException();
        }
    }
}
