using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using SDSMTGDT.GWorks.Physics.Collisions;
using SDSMTGDT.GWorks.Physics.Collisions.Routes;
using NUnit.Framework;

namespace SDSMTGDT.GWorks.Physics
{
    /// <summary>
    /// Object designed to test the Physics Manager's Collision system
    /// </summary>
    public class TestColliderB : BoundsCollidable
    {
        public TestColliderB(CollisionManager collisions, Rectangle bounds) : base(collisions, bounds)
        {
            var router = new CollisionEventRouter();
            router.addCollisionRoute(new TypeCollisionRoute<TestColliderB, TestColliderA>(testACollision));
            collisionPublisher.registerEventSubscriber(router);
        }

        public void testACollision(TypedCollisionEventInfo<TestColliderB, TestColliderA> info)
        {
            collided = true;
        }
    }
}
