using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Humble.Collisions
{
    public class RectHitBox : AHitBox
    {

        private Rectangle _rect;

        public int X
        {
            get { return _rect.X; }
            set { _rect.X = value; }
        }
        public int Y
         {
            get { return _rect.Y; }
            set { _rect.Y = value; }
        }
        public int Height
        {
            get { return _rect.Height; }
            set { _rect.Height = value; }
        }
        public int Width
        {
            get { return _rect.Width; }
            set { _rect.Width = value; }
        }

        public RectHitBox(int x, int y, int width, int height)
        {
            _rect = new Rectangle(x, y, width, height);
        }

        public override HitBoxType GetHitBoxType()
        {
            return HitBoxType.RECTANGLE;
        }

        public override bool Intersects(AHitBox hb)
        {
            if (hb.GetHitBoxType() == HitBoxType.RECTANGLE)
            {
                RectHitBox tmp = (RectHitBox)hb;
                if (tmp._rect.Intersects(_rect))
                {
                    return true;
                }
            }
            else if (hb.GetHitBoxType() == HitBoxType.CIRCLE)
            {
                CircleHitBox tmp = (CircleHitBox)hb;

                Vector2[] points = new Vector2[4] {
                    new Vector2(X, Y),
                    new Vector2(X + Width, Y),
                    new Vector2(X + Width, Y + Height),
                    new Vector2(X, Y + Height)
                };

                foreach (Vector2 pt in points)
                {
                    if (DistanceABNoSqrt(tmp.X, tmp.Y, pt.X, pt.Y) <= tmp.Radius * tmp.Radius)
                        return true;
                }

            }
            return false;
        }
    }
}
