using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Humble.Events
{
    public interface IClickable
    {
        Rectangle getClickableArea();

        /// <summary>
        /// The user released the mouse button on the clickable area
        /// </summary>
        void OnClick();

        /// <summary>
        /// The user pressed down the button on the clickable area
        /// </summary>
        void OnClickDown();

        /// <summary>
        /// The user released the mouse button
        /// </summary>
        void OnClickUp();

        /// <summary>
        /// The cursor entered the clickable area
        /// </summary>
        void OnHoverIn();

        /// <summary>
        /// The cursor exited the clickable area
        /// </summary>
        void OnHoverOut();
    }
}
