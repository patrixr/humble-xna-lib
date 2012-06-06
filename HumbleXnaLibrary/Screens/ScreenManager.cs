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
    /// Pile de screens
    /// Le ScreenManager fait appel aux méthodes Draw et Update de chaques screen, celles-ci doivent etre implementées
    /// Class en singleton, il ne peut y avoir qu'un unique screen manager par jeu.
    /// </summary>
    public class ScreenManager
    {
        private Stack<Screen> Screens = new Stack<Screen>();
        private Queue<ScreenManagerEvent> Events = new Queue<ScreenManagerEvent>();
        private int ToPop = 0;

        /// <summary>
        /// Renvoie le screen en haut de la pile
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

        #region SINGLETON

        private static ScreenManager singleton = null;

        /// <summary>
        /// Retourne l'instance de screenmanager, le crée au premier appel
        /// La property Singleton peut être utilisée alternativement
        /// </summary>
        /// <returns>Le screenmanager</returns>
        public static ScreenManager GetInstance()
        {
            if (singleton == null)
                singleton = new ScreenManager();
            return singleton;
        }

        /// <summary>
        /// Correspond a l'instance de screenmanager, sera crée a la première utilisation.
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
        /// Dessine chaque screen dans l'ordre de la stack, le plus haut en premier
        /// S'arrete au premier screen ayant la property BlocksDraw a true.
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
        /// Update chaque screen dans l'ordre de la stack, le plus haut en premier
        /// S'arrete au premier screen ayant la property BlocksUpdate a true.
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
        /// Rajoute un screen a la pile
        /// Attention, celui ci ne sera pas rajouté immediatement, mais avant le prochain update
        /// </summary>
        /// <param name="scr">Le screen a rajouter</param>
        public void pushScreen(Screen scr)
        {
            scr.Initialize();
            Events.Enqueue(new ScreenManagerEvent(ScreenManagerEvent.Type.PUSH, scr));
        }

        /// <summary>
        /// Pop un screen de la pile
        /// Attention, celui ci ne sera pas retiré immédiatement, mais avant le prochain update
        /// </summary>
        public void popScreen()
        {
            ToPop++;
            Events.Enqueue(new ScreenManagerEvent(ScreenManagerEvent.Type.POP));
        }

    }
}
