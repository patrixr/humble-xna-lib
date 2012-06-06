using System;
using System.Collections.Generic;
using System.Linq;
using Humble.Screens;
using Humble.Components;
using Humble.Components.Particles;
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
        #if DEBUG
        //FPSDisplay fpsDisplay;
        #endif

        protected GraphicsDeviceManager graphics;
        protected SpriteBatch spriteBatch;
        protected ScreenManager screenManager;

        public HumbleGame()
        {
            graphics = new GraphicsDeviceManager(this);
            screenManager = ScreenManager.GetInstance();
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            #if DEBUG
            //fpsDisplay = new FPSDisplay(Content.Load<SpriteFont>("fpsfont"));
            #endif

            // TODO: use this.Content to load your game content here
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here
            screenManager.Update(gameTime);

            #if DEBUG
            //fpsDisplay.Update(gameTime);
            #endif

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here
            screenManager.Draw();

            #if DEBUG
            //fpsDisplay.Draw(spriteBatch);
            #endif

            base.Draw(gameTime);
        }
    }
}
