using SDSMTGDT.GWorks.Physics.Collisions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using SDSMTGDT.GWorks.Physics;
using SDSMTGDT.GWorks.Events;

namespace BrickBreaker.Helpers
{
    //Decent collision zone
    abstract class CollisionZone : Collidable, IDisposable
    {
        protected Rectangle bounds;
        private CollisionManager collisions;

        public CollisionEventPublisher collisionPublisher { get; }

        internal CollisionZone(Rectangle bounds, CollisionManager collisions)
        {
            this.bounds = bounds;
            this.collisions = collisions;
            this.collisionPublisher = new CollisionEventPublisher(collisions, this);
        }

        public Rectangle getBounds()
        {
            return bounds;
        }

        public void Dispose()
        {
            collisions.unregisterCollidable(this);
        }
    }
}
