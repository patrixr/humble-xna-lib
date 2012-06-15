using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Humble.Configuration
{
    /// <summary>
    /// Contains properties that define the general behavior of Humble
    /// </summary>
    public static class HumbleConfiguration
    {
        /// <summary>
        /// Defines the maximum number of messages processed every frame
        /// Set to 0 to process all of them every time.
        /// </summary>
        public static uint MSG_MAX_PROCESSED_MESSAGES = 0;

    }
}
