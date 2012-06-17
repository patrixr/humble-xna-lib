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

        private float _scale;
        private float _rotation = 0;
        private Texture2D _texture;
        public SpriteSheet Sheet;

        private Vector2 _destination;
        private Vector2 _origin;
        private bool _centered;

        public AnimatedSprite(Texture2D texture, SpriteSheet ss, Vector2 position, bool centeredOrigin = true, float scale = 1f)
        {
            _texture = texture;
            _scale = scale;
            Sheet = ss;
            Position = position;
            _destination = new Vector2();
            _centered = centeredOrigin;
            _origin = new Vector2(0,0);
        }

        public void SetAnimationState(string state)
        {
            Sheet.SetState(state);
        }

        public override void Initialize()
        {
            ;
        }

        public void SetRotation(float r)
        {
            _rotation = r;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Rectangle source = Sheet.GetBlitArea();

            _destination.X = Position.X;
            _destination.Y = Position.Y;
            if (_centered)
            {
                _origin.X = source.Width / 2;
                _origin.Y = source.Height / 2;
                //_destination.X = Position.X - _scale * source.Width / 2;
                //_destination.Y = Position.Y - _scale * source.Height / 2;
            }
            else
            {
                //_destination.X = Position.X;
                //_destination.Y = Position.Y;
            }

            spriteBatch.Draw(_texture, _destination, source, Color.White, _rotation, _origin, _scale, SpriteEffects.None, 0);
        }

        public override void Update(GameTime gameTime)
        {
            Sheet.Update(gameTime);
        }
    }
}
