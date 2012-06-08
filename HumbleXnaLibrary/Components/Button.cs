using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Humble.Events;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Humble.Components
{
    public class Button : AComponent, IClickable
    {
        public delegate void ClickAction();

        private Texture2D _texture = null;
        private ClickAction _callback;
        private Color _hoverTint;
        private Color _clickTint;

        private bool _clicked = false;
        private bool _hovered = false;

        private Rectangle _clickableArea;
        private Vector2 _position;
        public Vector2 Position
        {
            get { return _position; }
            set
            {
                _clickableArea.X = (int)value.X;
                _clickableArea.Y = (int)value.Y;
                _position = value; 
            }
        }

        public Button(Texture2D tex, Vector2 pos, ClickAction callback, Color tintOnHover, Color tintOnClick) : base(true)
        {
            _callback = callback;
            _texture = tex;
            _position = pos;
            _hoverTint = tintOnHover;
            _clickTint = tintOnClick;
            _clickableArea = new Rectangle((int)pos.X, (int)pos.Y, tex.Width, tex.Height);

        }

        public Button(Texture2D tex, int x, int y, ClickAction callback, Color tintOnHover, Color tintOnClick)
            : base(true)
        {
            _callback = callback;
            _texture = tex;
            _position = new Vector2((float)x, (float)y);
            _hoverTint = tintOnHover;
            _clickTint = tintOnClick;
            _clickableArea = new Rectangle(x, y, tex.Width, tex.Height);
        }

        public Rectangle getClickableArea()
        {
            return _clickableArea;
        }

        public void OnClick()
        {
            _callback();
            _clicked = false;
        }

        public void OnClickDown()
        {
            _clicked = true;
        }

        public void OnClickUp()
        {
            _clicked = false;
        }

        public void OnHoverIn()
        {
            _hovered = true;
        }
        
        public void OnHoverOut()
        {
            _hovered = false;
        }

        public override void Initialize()
        {
            ;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Color tint = Color.White;

            if (_clicked)
                tint = _clickTint;
            else if (_hovered)
                tint = _hoverTint;

            spriteBatch.Draw(_texture, _clickableArea, tint);
        }

        public override void Update(GameTime gameTime)
        {
            ;
        }
    }
}
