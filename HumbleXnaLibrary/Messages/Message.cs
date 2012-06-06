using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Humble.Messages
{
    class Message
    {
        public Message(String id, Object param1, Object param2)
        {
            Id = id;
            param1 = Param1;
            param2 = Param2;
        }

        public String Id;
        public Object Param1;
        public Object Param2;
    }
}
