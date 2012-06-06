using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Humble.Screens
{
    class ScreenManagerEvent
    {
        public enum Type
        {
            POP,
            PUSH
        };


        public Type type;
        public Screen target;


        public ScreenManagerEvent(Type e, Screen sc = null)
        {
            type = e;
            target = sc;
        }

    }
}
