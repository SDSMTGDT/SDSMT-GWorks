using SDSMTGDT.GWorks.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDSMTGDT.GWorks.Physics.Collisions
{
    /// <summary>
    /// Publishes Collision Events
    /// </summary>
    public class CollisionEventPublisher: GameEventPublisher<CollisionEventInfo>
    {
        /// <summary>
        /// The collidable object associated with this publisher.
        /// The collider will be sent along with the object it collided with,
        /// when a collision event is fired
        /// </summary>
        private Collidable collider;

        /// <summary>
        /// Create a new CollisionEventPublisher to forward collision events
        /// to the event manager
        /// </summary>
        /// <param name="manager">The EventManager to publish events to</param>
        /// <param name="collider">The Collidable to publish events about</param>
        public CollisionEventPublisher(EventManager manager, Collidable collider) :
            base(manager, collider.ToString() + " Collider")
        {
            this.collider = collider;
        }

        /// <summary>
        /// Publish a collision event between the Collidable this publisher was initialized
        /// with and the Collidable provided in the function call.
        /// </summary>
        /// <param name="collided">The Collidable the collider collided with</param>
        public void publish(Collidable collided)
        {
            fireEvent(new CollisionEventInfo(collider, collided));
        }
    }
}
