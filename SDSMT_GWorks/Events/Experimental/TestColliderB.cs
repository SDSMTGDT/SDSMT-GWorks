using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace SDSMTGDT.GWorks.Events.Experimental
{
    public class TestColliderB : Collidable
    {
        public TestColliderB(SmallObjectCollider soc)
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
