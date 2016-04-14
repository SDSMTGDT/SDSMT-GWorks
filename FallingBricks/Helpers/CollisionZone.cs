using SDSMTGDT.GWorks.Physics.Collisions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using SDSMTGDT.GWorks.Physics;
using SDSMTGDT.GWorks.Events;

namespace FallingBricks.Helpers
{
    //Decent collision zone
    abstract class CollisionZone : Collidable, IDisposable
    {
        protected Rectangle bounds;
        protected PhysicsManager physics;
        protected GameEventHook<CollisionEventInfo> hook;

        internal CollisionZone(PhysicsManager physics)
        {
            this.bounds = new Rectangle();
            this.physics = physics;
            this.hook = physics.obtainCollisionHook(this);
        }

        public Rectangle getBounds()
        {
            return bounds;
        }

        protected void setBounds(Rectangle collisionBounds)
        {
            this.bounds = collisionBounds;
        }

        protected void setBounds(int x, int y, int width, int height)
        {
            this.bounds.X = x;
            this.bounds.Y = y;
            this.bounds.Width = width;
            this.bounds.Height = height;
        }

        public void Dispose()
        {
            physics.unregisterCollidableFromSystem(this);
        }
    }
}
