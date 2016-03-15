using SDSMTGDT.GWorks.Physics.Collisions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace SDSMTGDT.GWorks.Physics
{
    internal class BoundsCollidable : Collidable
    {
        private Rectangle bounds;

        public BoundsCollidable(Rectangle bounds)
        {
            this.bounds = bounds;
        }

        public Rectangle getBounds()
        {
            return bounds;
        }
    }
}
