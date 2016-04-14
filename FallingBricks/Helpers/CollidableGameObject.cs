using SDSMTGDT.GWorks.Physics.Collisions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SDSMTGDT.GWorks.Physics;
using SDSMTGDT.GWorks.Events;

namespace FallingBricks.Helpers
{
    abstract class CollidableGameObject : GameObject, Collidable, IDisposable
    {
        protected PhysicsManager physics;
        protected GameEventHook<CollisionEventInfo> hook;

        protected CollidableGameObject(PhysicsManager physics)
            : base()
        {
            this.physics = physics;
            this.hook = physics.obtainCollisionHook(this);
        }

        public void Dispose()
        {
            physics.unregisterCollidableFromSystem(this);
        }
    }
}
