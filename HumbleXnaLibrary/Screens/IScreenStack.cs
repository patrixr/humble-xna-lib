using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Humble.Screens
{
    interface IScreenStack
    {
        void pushScreen(Screen scr);
        void popScreen();
    }
}
