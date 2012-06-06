using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Humble.Components.Particles
{
    public interface IParticleGenerator
    {
        Particle GenerateParticle();
        Particle GenerateParticle(Vector2 position);
    }
}
