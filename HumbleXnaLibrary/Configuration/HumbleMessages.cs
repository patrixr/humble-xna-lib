using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Humble.Configuration
{
    /// <summary>
    /// Contains all builtin messages for easier access
    /// </summary>
    public static class HumbleMessages
    {
        /// <summary>
        /// When posted, will cause the game to exit
        /// </summary>
        public static readonly string MSG_BUILTIN_QUIT = "MSG_HUMBLE_QUIT";
        public static readonly string MSG_BUILTIN_POP_SCREEN = "MSG_HUMBLE_POP_SCREEN";
        public static readonly string MSG_BUILTIN_PUSH_SCREEN = "MSG_HUMBLE_PUSH_SCREEN";
    }
}
