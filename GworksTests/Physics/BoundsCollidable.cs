using SDSMTGDT.GWorks.Physics.Collisions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace SDSMTGDT.GWorks.Physics
{
    public class BoundsCollidable : Collidable
    {
        private Rectangle bounds;
        public bool collided { get; protected set; } = false;

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
