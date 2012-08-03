using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Humble.Messages
{
    class MessageType
    {
        public MessageType(String id, Boolean uniqueListener)
        {
            Id = id;
            UniqueListener = uniqueListener;
            Listeners = new List<IMessageObject>();
        }

        public bool AddListener(IMessageObject l)
        {
            if (UniqueListener && Listeners.Count != 0)
                return false;
            Listeners.Add(l);
            return true;
        }

        public bool RemoveListener(IMessageObject l)
        {
            return Listeners.Remove(l);
        }

        public String Id;
        public Boolean UniqueListener;
        public List<IMessageObject> Listeners;
    }
}
