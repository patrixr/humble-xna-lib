using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Humble.Collisions
{
    public enum HitBoxType
    {
        CIRCLE,
        RECTANGLE
    }

    public abstract class AHitBox
    {

        abstract public HitBoxType GetHitBoxType();

        abstract public bool Intersects(AHitBox hb);

        // UTILS
        protected int DistanceABNoSqrt(float xa, float ya, float xb, float yb)
        {
            return (int)(Math.Pow((double)xb - (double)xa, 2) + Math.Pow((double)yb - (double)ya, 2));
        }

    }
}
