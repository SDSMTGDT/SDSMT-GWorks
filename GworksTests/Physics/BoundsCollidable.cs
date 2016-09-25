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

        public CollisionEventPublisher collisionPublisher { get; }

        public BoundsCollidable(CollisionManager collisions, Rectangle bounds)
        {
            this.bounds = bounds;
            this.collisionPublisher = new CollisionEventPublisher(collisions, this);
        }

        public Rectangle getBounds()
        {
            return bounds;
        }
    }
}
