using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Humble.Messages
{
    /// <summary>
    /// A message object can recieve a callback after a message has been posted
    /// </summary>
    public interface IMessageObject
    {
        /// <summary>
        /// This function is called when processing a message for which the current object is listed as a listener
        /// </summary>
        /// <param name="msg">Name of the message</param>
        /// <param name="param1">First parameter, may require a cast</param>
        /// <param name="param2">Second parameter, may require a cast</param>
        void HandleCallback(String msg, Object param1, Object param2);

    }
}
