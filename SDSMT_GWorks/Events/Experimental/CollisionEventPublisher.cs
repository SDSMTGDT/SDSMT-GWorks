using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDSMTGDT.GWorks.Events.Experimental
{
    public class CollisionEventPublisher: GameEventPublisher<CollisionEventInfo>
    {
        private Collidable collider;
        public CollisionEventPublisher(EventManager manager, Collidable collider) :
            base(manager, collider.ToString() + " Collider")
        {
            this.collider = collider;
        }

        public void publish(Collidable collided)
        {
            fireEvent(new CollisionEventInfo(collider, collided));
        }
    }
}
