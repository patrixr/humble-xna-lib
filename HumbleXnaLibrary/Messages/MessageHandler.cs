using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Humble.Configuration;
using Microsoft.Xna.Framework;

namespace Humble.Messages
{
    /// <summary>
    /// Singleton used to process every posted messages and call the appropriate callbacks.
    /// It's behavior can be manipulated through the HumbleConfiguration static class.
    /// Warning : Messages must be used with careful consideration
    /// </summary>
    public class MessageHandler
    {
        #region SINGLETON

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

        #region PROPERTIES
        Dictionary<String, MessageType> _messageTypes = new Dictionary<string, MessageType>();
        List<Message> _pendingDelayedMessages = new List<Message>();
        Queue<Message> _pendingMessages = new Queue<Message>();
        #endregion

        #region PRIVATE METHODS

        private void ProcessSingleMessage(Message msg)
        {
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
        }

        #endregion

        #region PUBLIC METHODS

        /// <summary>
        /// Processes the enqueued messages, the listeners will be notified.
        /// This function is called from the HumbleGame main loop, and must not be called elsewhere. Unless used in an alternate main loop.
        /// </summary>
        public void ProcessMessages(int elapsed_ms)
        {
            int count = 0;

            for (int i = 0; i < _pendingDelayedMessages.Count; i++)
            {
                Message msg = _pendingDelayedMessages[i];
                msg.Tick += (uint)elapsed_ms;
                if (msg.Tick >= msg.Delay)
                {
                    _pendingMessages.Enqueue(msg);
                    _pendingDelayedMessages.RemoveAt(i--);
                }
            }

            while (_pendingMessages.Count > 0)
            {
                Message msg = _pendingMessages.Dequeue();
                ProcessSingleMessage(msg);
                msg.Dispose();

                if (HumbleConfiguration.MSG_MAX_PROCESSED_MESSAGES > 0)
                {
                    count++;
                    if (count > HumbleConfiguration.MSG_MAX_PROCESSED_MESSAGES)
                        break;
                }
            }
        }

        /// <summary>
        /// Processes the enqueued messages, the listeners will be notified.
        /// This function is called from the HumbleGame main loop, and must not be called elsewhere. Unless used in an alternate main loop.
        /// </summary>
        public void ProcessMessages(GameTime gameTime)
        {
            ProcessMessages(gameTime.ElapsedGameTime.Milliseconds);
        }

        /// <summary>
        /// Creates a message type, that can later be posted
        /// A message cannot be posted unless created first
        /// </summary>
        /// <param name="msg">Name assigned to the message</param>
        /// <param name="uniqueListener">Defines whether multiple listener may be registered for that message</param>
        /// <returns>True upon creation</returns>
        public bool CreateMessage(String msg, bool uniqueListener)
        {
            _messageTypes.Add(msg, new MessageType(msg, uniqueListener));

            return true;
        }

        /// <summary>
        /// Registers a listener for a particular message
        /// When posted, the listener will be alerted by the HandleCallback method
        /// </summary>
        /// <param name="listener">the listener object</param>
        /// <param name="msg">Message to register to</param>
        /// <returns></returns>
        public bool RegisterListener(IMessageObject listener, String msg)
        {
            if (!_messageTypes.ContainsKey(msg))
            {
                return false;
            }
            return _messageTypes[msg].AddListener(listener);
        }

        /// <summary>
        /// Removes a listener for a particular message
        /// </summary>
        /// <param name="listener">the listener object</param>
        /// <param name="msg">Message to register to</param>
        /// <returns></returns>
        public bool RmoveListener(IMessageObject listener, String msg)
        {
            if (!_messageTypes.ContainsKey(msg))
            {
                return false;
            }
            return _messageTypes[msg].RemoveListener(listener);
        }

        /// <summary>
        /// Adds a message to the pending messages queue.
        /// </summary>
        /// <param name="msg">Message to post</param>
        /// <param name="param1"></param>
        /// <param name="param2"></param>
        /// <returns></returns>
        public bool PostMessage(String msg, Object param1 = null, Object param2 = null)
        {
            _pendingMessages.Enqueue(new Message(msg, param1, param2));

            return true;
        }

        /// <summary>
        /// Adds a delayed message to the pending messages queue, will be processed after delay_ms seconds.
        /// </summary>
        /// <param name="msg">Message to post</param>
        /// <param name="param1"></param>
        /// <param name="param2"></param>
        /// <param name="delay_ms"></param>
        /// <returns></returns>
        public bool PostDelayedMessage(String msg, Object param1, Object param2, uint delay_ms)
        {
            _pendingDelayedMessages.Add(new Message(msg, param1, param2, delay_ms));

            return true;
        }

        #endregion

    }
}
