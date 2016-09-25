using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using SDSMTGDT.GWorks.Physics;
using SDSMTGDT.GWorks.Physics.Collisions;
using SDSMTGDT.GWorks.Events;

namespace Pong.BaseClasses
{
    abstract class CollisionZone : Collidable
    {
        protected Rectangle bounds;
        private CollisionManager collisions;
        public CollisionEventPublisher collisionPublisher { get; }

        protected CollisionZone(Rectangle bounds, CollisionManager collisions)
        {
            this.bounds = bounds;
            this.collisions = collisions;
            this.collisionPublisher = new CollisionEventPublisher(collisions, this);
        }

        public Rectangle getBounds()
        {
            return bounds;
        }

        public void dispose()
        {
            collisions.unregisterCollidable(this);
        }
    }
}
