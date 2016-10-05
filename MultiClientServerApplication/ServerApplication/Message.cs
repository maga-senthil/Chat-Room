using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerApplication
{
    class Message
    {
        public Client messageSender;
        public string bodyOfMessage;
        public string UserId;

        public Message(Client MessageSender,string BodyOfMessage)
        {
            messageSender = MessageSender;
            this.bodyOfMessage = BodyOfMessage;
            UserId = MessageSender?.userId;
        }
    }
}
