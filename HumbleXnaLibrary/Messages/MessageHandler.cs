using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Humble.Messages
{
    /// <summary>
    /// The Message Handler processes every posted messages and calls the appropriate callbacks
    /// </summary>
    public class MessageHandler
    {
        #region SINGLETON

        /// <summary>
        /// Private constructor
        /// </summary>
        private MessageHandler()
        {
        }

        private static MessageHandler singleton = null;

        /// <summary>
        /// Get the single instance of the Message Handler, will create it on first call.
        /// </summary>
        /// <returns>The single instance of the MessageHandler</returns>
        public static MessageHandler GetInstance()
        {
            if (singleton == null)
                singleton = new MessageHandler();
            return singleton;
        }

        /// <summary>
        /// Single instance of the MessageHandler
        /// </summary>
        public static MessageHandler Singleton
        {
            get
            {
                if (singleton == null)
                    singleton = new MessageHandler();
                return singleton;
            }
            private set
            {
                singleton = value;
            }
        }

        #endregion

        Dictionary<String, MessageType> _messageTypes = new Dictionary<string, MessageType>();
        Queue<Message> _pendingMessages = new Queue<Message>();

        public void ProcessMessages()
        {
            while (_pendingMessages.Count > 0)
            {
                Message msg = _pendingMessages.Dequeue();
                MessageType type = _messageTypes[msg.Id];

                if (type.UniqueListener && type.Listeners.Count > 0)
                {
                    type.Listeners[0].HandleCallback(msg.Id, msg.Param1, msg.Param2);
                }
                else if (type.Listeners.Count > 0)
                {
                    foreach (IMessageObject l in type.Listeners)
                        l.HandleCallback(msg.Id, msg.Param1, msg.Param2);
                }
                msg.Dispose();
            }
        }

        public bool CreateMessage(String name, bool uniqueListener)
        {
            _messageTypes.Add(name, new MessageType(name, uniqueListener));

            return true;
        }

        public bool RegisterListener(IMessageObject listener, String msgname)
        {
            if (!_messageTypes.ContainsKey(msgname))
            {
                return false;
            }
            return _messageTypes[msgname].AddListener(listener);
        }

        public bool PostMessage(String name, Object param1 = null, Object param2 = null)
        {
            _pendingMessages.Enqueue(new Message(name, param1, param2));

            return true;
        }


    }
}
