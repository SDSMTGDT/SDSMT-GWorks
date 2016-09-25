using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using SDSMTGDT.GWorks.Physics.Collisions;
using SDSMTGDT.GWorks.Physics.Collisions.Routes;

namespace SDSMTGDT.GWorks.Physics
{
    /// <summary>
    /// Object designed to test the Physics Manager's Collision system
    /// </summary>
    public class TestColliderA : BoundsCollidable
    {
        public TestColliderA(CollisionManager collisions, Rectangle bounds) : base(collisions, bounds)
        {
            var router = new CollisionEventRouter();
            router.addCollisionRoute(new TypeCollisionRoute<TestColliderA, TestColliderB>(testBCollision));
            collisionPublisher.registerEventSubscriber(router);
        }

        /// <summary>
        /// Code to be called when this object collides with a testColliderB
        /// </summary>
        /// <param name="info">collision information</param>
        public void testBCollision(TypedCollisionEventInfo<TestColliderA, TestColliderB> info)
        {
            collided = true;
        }
    }
}
