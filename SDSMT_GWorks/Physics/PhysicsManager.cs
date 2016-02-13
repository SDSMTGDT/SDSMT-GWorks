using SDSMTGDT.GWorks.Events;
using SDSMTGDT.GWorks.Physics.Collisions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDSMTGDT.GWorks.Physics
{
    public class PhysicsManager
    {
        private EventManager eventManager;
        private Dictionary<Collidable, CollisionEventPublisher> publishers;
        public PhysicsManager(EventManager eventManager)
        {
            this.eventManager = eventManager;
            this.publishers = new Dictionary<Collidable, CollisionEventPublisher>();
        }

        public CollisionEventPublisher getCollisionPublisher(Collidable collider)
        {
            if (publishers[collider] == null)
                publishers[collider] = new CollisionEventPublisher(eventManager, collider);
            return publishers[collider];            
        }

        private void collide(Collidable collider, Collidable collided)
        {
            publishers[collider]?.publish(collided);
            publishers[collided]?.publish(collider);
        }
    }
}
