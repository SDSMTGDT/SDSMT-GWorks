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
        private PhysicsManager physics;
        protected GameEventHook<CollisionEventInfo> hook;

        protected CollisionZone(Rectangle bounds, PhysicsManager physics)
        {
            this.bounds = bounds;
            this.physics = physics;
            hook = physics.obtainCollisionHook(this);
        }

        public Rectangle getBounds()
        {
            return bounds;
        }

        public void dispose()
        {
            physics.unregisterCollidableFromSystem(this);
        }
    }
}
