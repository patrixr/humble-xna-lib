using System;
using System.Collections.Generic;
using System.Linq;
using Humble.Screens;
using Humble.Components;
using Humble.Components.Particles;
using Humble.Messages;
using Humble.Events;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Humble.Configuration;

namespace Humble
{
    public delegate void Callback();
    
    /// <summary>
    /// Base class of a humble game
    /// </summary>
    public class HumbleGame : Microsoft.Xna.Framework.Game, IMessageObject
    {
        /// <summary>
        /// Graphics Device Manager
        /// </summary>
        protected GraphicsDeviceManager graphics;
        /// <summary>
        /// SpriteBatch
        /// </summary>
        protected SpriteBatch spriteBatch;
        /// <summary>
        /// ScreenManager
        /// </summary>
        protected ScreenManager screenManager;
        /// <summary>
        /// Message Handler
        /// </summary>
        protected MessageHandler messageHandler;

        public HumbleGame()
        {
            graphics = new GraphicsDeviceManager(this);
            screenManager = ScreenManager.GetInstance();
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            messageHandler = MessageHandler.Singleton;
            messageHandler.CreateMessage(HumbleMessages.MSG_BUILTIN_QUIT, true);
            messageHandler.RegisterListener(this, HumbleMessages.MSG_BUILTIN_QUIT);
            messageHandler.CreateMessage(HumbleMessages.MSG_BUILTIN_PUSH_SCREEN, true);
            messageHandler.RegisterListener(this, HumbleMessages.MSG_BUILTIN_PUSH_SCREEN);
            messageHandler.CreateMessage(HumbleMessages.MSG_BUILTIN_POP_SCREEN, true);
            messageHandler.RegisterListener(this, HumbleMessages.MSG_BUILTIN_POP_SCREEN);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            base.LoadContent();
        }

        protected override void UnloadContent()
        {
            base.UnloadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            messageHandler.ProcessMessages(gameTime);
            screenManager.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            screenManager.Draw();

            base.Draw(gameTime);
        }

        public void HandleCallback(string msg, object param1, object param2)
        {
            if (msg == HumbleMessages.MSG_BUILTIN_QUIT)
            {
                screenManager.Clear();
                this.Exit();
            }
            else if (msg == HumbleMessages.MSG_BUILTIN_POP_SCREEN)
                screenManager.popScreen();
            else if (msg == HumbleMessages.MSG_BUILTIN_PUSH_SCREEN && param1 != null)
                screenManager.pushScreen((Screen)param1);
        }
    }
}
