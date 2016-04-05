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
        private PhysicsManager physics;
        protected GameEventHook<CollisionEventInfo> hook;

        internal CollisionZone(Rectangle bounds, PhysicsManager physics)
        {
            this.bounds = bounds;
            this.physics = physics;
            this.hook = physics.obtainCollisionHook(this);
        }

        public Rectangle getBounds()
        {
            return bounds;
        }

        public void Dispose()
        {
            physics.unregisterCollidableFromSystem(this);
        }
    }
}
