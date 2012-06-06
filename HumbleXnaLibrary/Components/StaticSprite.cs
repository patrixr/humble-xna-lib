using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Humble.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Humble.Components
{
    public class StaticSprite : AComponent
    {
        private Vector2 position;
        private Texture2D texture;

        public StaticSprite(Texture2D tex, Vector2 pos) : base()
        {
            position = pos;
            texture = tex;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }

        public override void Update(GameTime gameTime)
        {
            return;
        }

        public override void Initialize()
        {
            return;
        }
    }
}
