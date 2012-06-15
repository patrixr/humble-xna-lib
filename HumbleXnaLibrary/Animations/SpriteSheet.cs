using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Humble.Exceptions;

namespace Humble.Animations
{
    /// <summary>
    /// Specifies the different frames and animations on a corresponding texture
    /// 
    /// </summary>
    public class SpriteSheet
    {
        #region CONTENT

        public String texturename;
        public List<AnimationState> states;

        #endregion

        #region ANIMATION

        private Rectangle _bliteArea;
        private AnimationState _currentState;

        private int _currentFrame = 0;
        private int _tick = 0;
        private bool _frameHasChanged = true;

        #endregion

        #region PRIVATE

        private bool _hasBeenValidated;

        private AnimationState GetState(string name)
        {
            foreach (AnimationState st in states)
                if (st.name == name)
                    return st;
            return null;
        }

        private void Validate()
        {
            if (_hasBeenValidated)
                return;

            bool hasNormal = false;

            int y_offset = 0;
            foreach (AnimationState st in states)
            {
                if (st.name == "normal")
                    hasNormal = true;
                if (GetState(st.nextstate) == null)
                    throw new AnimationOperationException("The next state " + st.nextstate + " of " + st.name + " has not been defined");
                if (st.framecount <= 0)
                    throw new AnimationOperationException("Invalid frame count on state " +  st.name);
                if (st.switchdelay < 0)
                    throw new AnimationOperationException("Invalid switch delay on state " + st.name);
                if (st.height < 0)
                    throw new AnimationOperationException("Invalid height on state " + st.name);
                if (st.width < 0)
                    throw new AnimationOperationException("Invalid width on state " + st.name);
                st.SetYOffset(y_offset);
                y_offset += st.height;
            }

            if (!hasNormal)
                throw new AnimationOperationException("The spritesheet requires the animation state 'normal'");

            _currentState = GetState("normal");
            _hasBeenValidated = true;
        }

        #endregion

        #region PUBLIC

        public SpriteSheet()
        {
            _bliteArea = new Rectangle();
            _hasBeenValidated = false;
        }

        public void Update(GameTime gameTime)
        {
            Update(gameTime.ElapsedGameTime.Milliseconds);
        }

        public void Update(int elapsed)
        {
            Validate();

            _tick += elapsed;
            if (_tick >= _currentState.switchdelay)
            {
                _currentFrame++;
                if (_currentFrame >= _currentState.framecount)
                {
                    if (_currentState.IsLooped())
                        _currentFrame = 0;
                    else
                        _currentState = GetState(_currentState.nextstate);
                }
                _frameHasChanged = true;
                _tick = 0;
            }
        }

        public Rectangle GetBlitArea()
        {
            Validate();

            if (_frameHasChanged)
            {
                _bliteArea.Y = _currentState.GetYOffset();
                _bliteArea.X = _currentState.width * _currentFrame;
                _bliteArea.Height = _currentState.height;
                _bliteArea.Width = _currentState.width;
                _frameHasChanged = false;
            }

            return _bliteArea;
        }

        public bool IsOnLastFrame()
        {
            return (_currentFrame == _currentState.framecount - 1);
        }


        public void SetState(string state)
        {
            Validate();

            AnimationState tmp = GetState(state);
            if (tmp == null)
                throw new AnimationOperationException("Bad animation state : " + state);
            else
            {
                _currentState = tmp;
                _tick = 0;
                _currentFrame = 0;
                _frameHasChanged = true;
            }
        }

        #endregion
    }
}
