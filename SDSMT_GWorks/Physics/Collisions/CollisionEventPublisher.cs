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
        private readonly Collidable collider;
        private readonly CollisionManager collisions;

        /// <summary>
        /// Create a new CollisionEventPublisher to forward collision events
        /// to the event manager
        /// </summary>
        /// <param name="collisions">The collisions manager to publish events to</param>
        /// <param name="collider">The Collidable to publish events about</param>
        public CollisionEventPublisher(CollisionManager collisions, Collidable collider) :
            base(collisions.EventManager, collider.ToString() + " Collider")
        {
            this.collider = collider;
            this.collisions = collisions;
        }

        /// <summary>
        /// Publish a collision event between the Collidable this publisher was initialized
        /// with and the Collidable provided in the function call.
        /// </summary>
        /// <param name="collided">The collidable the collider collided with</param>
        /// <param name="group">The group containing one or both of the collidables</param>
        internal void Publish(Collidable collided, CollisionGroup group)
        {
            FireEvent(new CollisionEventInfo(collider, collided, group, collisions));
        }

        /// <summary>
        /// Returns the collidable that this publisher is registered to
        /// </summary>
        /// <returns>The collidable this publisher is registered to</returns>
        internal Collidable GetCollidable()
        {
            return collider;
        }
    }
}
