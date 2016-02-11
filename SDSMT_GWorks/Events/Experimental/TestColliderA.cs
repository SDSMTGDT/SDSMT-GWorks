using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace SDSMTGDT.GWorks.Events.Experimental
{
    public class TestColliderA : Collidable
    {
        public TestColliderA(SmallObjectCollider soc)
        {
            var publisher = soc.getCollisionPublisher(this);
            var router = new CollisionEventRouter();
            router.addCollisionRoute(new TypeCollisionRoute<TestColliderB>(testBCollision));
            publisher.registerEventSubscriber(router);
        }
        public Rectangle getBounds()
        {
            throw new NotImplementedException();
        }

        public void testBCollision(Collidable a, Collidable b)
        {

        }
    }
}
