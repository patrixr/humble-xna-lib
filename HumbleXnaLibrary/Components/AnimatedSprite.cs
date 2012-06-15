using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Humble.Animations;

namespace Humble.Components
{
    public class AnimatedSprite : AComponent
    {

        public Vector2 Position;

        private Texture2D _texture;
        public SpriteSheet Sheet;

        private Rectangle _destination;
        private bool _centered;

        public AnimatedSprite(Texture2D texture, SpriteSheet ss, Vector2 position, bool centeredOrigin = true)
        {
            _texture = texture;
            Sheet = ss;
            Position = position;
            _destination = new Rectangle();
            _centered = centeredOrigin;
        }

        public void SetAnimationState(string state)
        {
            Sheet.SetState(state);
        }

        public override void Initialize()
        {
            ;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Rectangle source = Sheet.GetBlitArea();

            if (_centered)
            {
                _destination.X = (int)Position.X - source.Width / 2;
                _destination.Y = (int)Position.Y - source.Height / 2;
            }
            else
            {
                _destination.X = (int)Position.X;
                _destination.Y = (int)Position.Y;
            }
            _destination.Width = source.Width;
            _destination.Height = source.Height;

            spriteBatch.Draw(_texture, _destination, source, Color.White);
        }

        public override void Update(GameTime gameTime)
        {
            Sheet.Update(gameTime);
        }
    }
}
