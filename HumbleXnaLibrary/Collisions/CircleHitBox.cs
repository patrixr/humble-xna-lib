using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Humble.Collisions
{
    public class CircleHitBox : AHitBox
    {
        public int X;
        public int Y;
        public int Radius;

        public CircleHitBox(int x, int y, int radius)
        {
            X = x;
            Y = y;
            Radius = radius;
        }

        public override HitBoxType GetHitBoxType()
        {
            return HitBoxType.CIRCLE;
        }

        public override bool Intersects(AHitBox hb)
        {
            if (hb.GetHitBoxType() == HitBoxType.RECTANGLE)
            {
                RectHitBox tmp = (RectHitBox)hb;
                return tmp.Intersects(this);
            }
            else if (hb.GetHitBoxType() == HitBoxType.CIRCLE)
            {
                CircleHitBox tmp = (CircleHitBox)hb;
                return (DistanceABNoSqrt(X, Y, tmp.X, tmp.Y) <= (Radius + tmp.Radius) * (Radius + tmp.Radius));
            }
            return false;
        }
    }
}
