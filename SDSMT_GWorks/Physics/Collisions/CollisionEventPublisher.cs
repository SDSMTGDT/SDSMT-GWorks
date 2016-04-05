using SDSMTGDT.GWorks.Events;
using SDSMTGDT.GWorks.Physics.Collisions.DataStructures;
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
        private PhysicsManager physics;

        /// <summary>
        /// Create a new CollisionEventPublisher to forward collision events
        /// to the event manager
        /// </summary>
        /// <param name="manager">The EventManager to publish events to</param>
        /// <param name="collider">The Collidable to publish events about</param>
        internal CollisionEventPublisher(EventManager manager, PhysicsManager physics, Collidable collider) :
            base(manager, collider.ToString() + " Collider")
        {
            this.collider = collider;
            this.physics = physics;
        }

        /// <summary>
        /// Publish a collision event between the Collidable this publisher was initialized
        /// with and the Collidable provided in the function call.
        /// </summary>
        /// <param name="collided">The collidable the collider collided with</param>
        /// <param name="group">The group containing one or both of the collidables</param>
        internal void publish(Collidable collided, CollisionGroup group)
        {
            fireEvent(new CollisionEventInfo(collider, collided, group, physics));
        }

        /// <summary>
        /// Returns the collidable that this publisher is registered to
        /// </summary>
        /// <returns>The collidable this publisher is registered to</returns>
        internal Collidable getCollidable()
        {
            return collider;
        }
    }
}
