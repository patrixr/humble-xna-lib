using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Humble.Screens
{
    class LinkedScreens : Screen, IScreenStack
    {
        private Stack<Screen> _stack;

        public LinkedScreens(HumbleGame game, bool visible) : base(game, visible)
        {
            _stack = new Stack<Screen>();
        }

        public void pushScreen(Screen scr)
        {
            _stack.Push(scr);
        }

        public void popScreen()
        {
            _stack.Pop();
        }

        public override void Draw()
        {
            foreach (Screen s in _stack)
                s.Draw();

            base.Draw();
        }

        public override void HandleInput()
        {
            foreach (Screen s in _stack)
                s.HandleInput();

            base.HandleInput();
        }

        public override void Initialize()
        {
            foreach (Screen s in _stack)
                s.Initialize();

            base.Initialize();
        }

        public override void UnloadContent()
        {
            foreach (Screen s in _stack)
                s.UnloadContent();
            
            base.UnloadContent();
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            foreach (Screen s in _stack)
                s.Update(gameTime);

            base.Update(gameTime);
        }
    }
}