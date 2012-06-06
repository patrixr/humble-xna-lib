using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Humble.Messages
{
    class Message : IDisposable
    {
        public Message(String id, Object param1, Object param2)
        {
            Id = id;
            param1 = Param1;
            param2 = Param2;
            Delay = 0;
            Tick = 0;
        }

        public Message(String id, Object param1, Object param2, uint delay_ms)
        {
            Id = id;
            param1 = Param1;
            param2 = Param2;
            Delay = delay_ms;
            Tick = 0;
        }

        
        // Content infos
        public String Id;
        public Object Param1;
        public Object Param2;
        // Timing infos
        public uint Delay;
        public uint Tick;

        public void Dispose()
        {
            // check gc's behavior
            Id = null;
            Param1 = null;
            Param2 = null;
        }
    }
}
