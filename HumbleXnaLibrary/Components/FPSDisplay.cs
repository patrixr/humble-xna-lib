using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Diagnostics;


namespace Humble.Components
{
   public class FPSDisplay : AComponent
    {
        private int FrameCount = 0;
        private SpriteFont Font;
        private Stopwatch timer = Stopwatch.StartNew();
        private float fps = 0;

        public FPSDisplay(SpriteFont font) : base()
        {
            Font = font;
        }

        public override void Initialize()
        {
        }

        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            this.FrameCount++;
            spriteBatch.Begin();
            spriteBatch.DrawString(Font, fps.ToString() + " Fps", Vector2.Zero, Color.White);
            spriteBatch.End();
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            if (timer.Elapsed > TimeSpan.FromSeconds(1))
            {
                fps = (float)(FrameCount / timer.Elapsed.TotalSeconds);
                timer.Restart();
                FrameCount = 0;
            }
        }
    }
}
