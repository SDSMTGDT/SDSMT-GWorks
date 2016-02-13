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
    public class TestColliderB : Collidable
    {
        public TestColliderB(PhysicsManager soc)
        {
            var publisher = soc.getCollisionPublisher(this);
            var router = new CollisionEventRouter();
            router.addCollisionRoute(new TypeCollisionRoute<TestColliderA>(testACollision));
            publisher.registerEventSubscriber(router);
        }
        public Rectangle getBounds()
        {
            throw new NotImplementedException();
        }

        public void testACollision(Collidable A, Collidable B)
        {

        }
    }
}
