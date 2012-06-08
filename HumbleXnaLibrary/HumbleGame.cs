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

namespace Humble
{
    public class HumbleGame : Microsoft.Xna.Framework.Game
    {
        protected GraphicsDeviceManager graphics;
        protected SpriteBatch spriteBatch;
        protected ScreenManager screenManager;
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
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            screenManager.Update(gameTime);
            messageHandler.ProcessMessages(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            screenManager.Draw();

            base.Draw(gameTime);
        }
    }
}
