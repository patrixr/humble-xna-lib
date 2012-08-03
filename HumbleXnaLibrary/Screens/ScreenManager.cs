using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;

namespace Humble.Screens
{
    /// <summary>
    /// Manages a stack of screens
    /// Calls the draw and update methods of each screen. They must be implemented.
    /// The Screen manager is a singleton. Only one can exist in each game
    /// </summary>
    public class ScreenManager : IScreenStack
    {
        private Stack<Screen> Screens = new Stack<Screen>();
        private Queue<ScreenManagerEvent> Events = new Queue<ScreenManagerEvent>();
        private int ToPop = 0;

        /// <summary>
        /// Returns the screen on top of the stack
        /// </summary>
        public Screen TopScreen
        {
            get
            {
                if (Screens.Count == 0)
                    return null;
                return Screens.Peek();
            }
        }

        public int Count
        {
            get { return Screens.Count; }
            set { }
        }

        #region SINGLETON

        private static ScreenManager singleton = null;

        /// <summary>
        /// Returns the sole instance of the screen manager.
        /// The singleton property may be used alternatively
        /// </summary>
        /// <returns>The screen manager</returns>
        public static ScreenManager GetInstance()
        {
            if (singleton == null)
                singleton = new ScreenManager();
            return singleton;
        }

        /// <summary>
        /// Instance of the screen manager, will be created on first use.
        /// </summary>
        public static ScreenManager Singleton
        {
            get
            {
                if (singleton == null)
                    singleton = new ScreenManager();
                return singleton;
            }
            private set
            {
                singleton = value;
            }
        }

        #endregion

        private ScreenManager()
        {
        }

        /// <summary>
        /// Draws every screen in the stack from the top down
        /// Stops at the first screen with the BlocksDraw property enabled
        /// </summary>
        public void Draw()
        {
            if (Screens.Count == 0)
                return;
            int first = 0;

            foreach (Screen scr in Screens)
            {
                if (scr.BlocksDraw)
                    break;
                ++first;
            }
            while (first >= 0)
            {
                if (first <= Screens.Count - 1 && Screens.ElementAt(first).Visible)
                    Screens.ElementAt(first).Draw();
                --first;
            }
        }

        /// <summary>
        /// Updates every screen in the stack from the top down
        /// Stops at the first screen with the BlocksUpdate property enabled
        /// </summary>
        public void Update(GameTime gameTime)
        {
            // Update Screen Stack
            while (Events.Count > 0)
            {
                ScreenManagerEvent ev = Events.Peek();

                if (ev.type == ScreenManagerEvent.Type.POP && ToPop > 0)
                {
                    if (Screens.Count > 0)
                    {
                        Screens.Peek().UnloadContent();
                        Screens.Pop();
                    }
                    ToPop--;
                }
                else
                {
                    if (ev.target != null)
                    {
                        Screens.Push(ev.target);
                    }
                }
                Events.Dequeue();
            }

            foreach (Screen scr in Screens)
            {
                scr.HandleInput();
                if (scr.BlocksInput)
                    break;
            }
            foreach (Screen scr in Screens)
            {
                scr.Update(gameTime);
                if (scr.BlocksUpdate)
                    break;
            }
        }

        /// <summary>
        /// Adds a screen to the stack
        /// Important, the screen will not be added immediatly. Changes on the stack will take effect during the next update
        /// </summary>
        /// <param name="scr">Le screen a rajouter</param>
        public void pushScreen(Screen scr)
        {
            scr.Initialize();
            Events.Enqueue(new ScreenManagerEvent(ScreenManagerEvent.Type.PUSH, scr));
        }

        /// <summary>
        /// Pops a screen from the stack
        /// Important, the screen will not be removed immediatly. Changes on the stack will take effect during the next update
        /// </summary>
        public void popScreen()
        {
            ToPop++;
            Events.Enqueue(new ScreenManagerEvent(ScreenManagerEvent.Type.POP));
        }

        /// <summary>
        /// Empty the screen stack
        /// </summary>
        public void Clear()
        {
            while (Screens.Count > 0)
            {
                Screens.Peek().UnloadContent();
                Screens.Pop();
            }
        }

    }
}
