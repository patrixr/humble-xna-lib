using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Humble.Animations
{
    /// <summary>
    /// This class is used by the SpriteSheet class, it defines an animation state
    /// </summary>
    public class AnimationState
    {

        public string name { get; set; }
        public int switchdelay { get; set; }
        public int width { get; set; }
        public int height { get; set; }
        public string nextstate { get; set; }
        public int framecount { get; set; }

        public bool IsLooped()
        {
            return (name == nextstate);
        }

        private int _y_offset = 0;

        public void SetYOffset(int y)
        {
            _y_offset = y;
        }

        public int GetYOffset()
        {
            return _y_offset;
        }


    }
}
