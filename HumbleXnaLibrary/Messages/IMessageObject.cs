using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Humble.Messages
{
    public interface IMessageObject
    {

        void HandleCallback(String msg, Object param1, Object param2);

    }
}
