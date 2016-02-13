using SDSMTGDT.GWorks.Events;
using SDSMTGDT.GWorks.Physics.Collisions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDSMTGDT.GWorks.Physics
{
    /// <summary>
    /// Handles moving bodies and their collisions.
    /// Interfaces with the EventManager to provide collision information to the outside world
    /// </summary>
    public class PhysicsManager
    {
        /// <summary>
        /// The event manager to forward collision events to.
        /// </summary>
        private EventManager eventManager;

        /// <summary>
        /// Mapping between collidable objects and the publishers for their collision events
        /// </summary>
        private Dictionary<Collidable, CollisionEventPublisher> publishers;

        /// <summary>
        /// Create a new PhysicsManager which publishes to the given EventManager
        /// </summary>
        /// <param name="eventManager">The EventManager to publish collision information to</param>
        public PhysicsManager(EventManager eventManager)
        {
            this.eventManager = eventManager;
            this.publishers = new Dictionary<Collidable, CollisionEventPublisher>();
        }

        /// <summary>
        /// Returns the collision event publisher associated with a given collidable.
        /// If the publisher does not exist, a new one will be created.
        /// </summary>
        /// <param name="collider">The object that will be colliding with others</param>
        /// <returns></returns>
        public CollisionEventPublisher getCollisionPublisher(Collidable collider)
        {
            if (publishers[collider] == null)
                publishers[collider] = new CollisionEventPublisher(eventManager, collider);
            return publishers[collider];            
        }

        /// <summary>
        /// Used to publish collision events between two collidables.
        /// Note: The collisions are symmetric and will be called on each object.
        /// </summary>
        /// <param name="collider">The first object in the collision</param>
        /// <param name="collided">The second object in the collision</param>
        private void collide(Collidable collider, Collidable collided)
        {
            publishers[collider]?.publish(collided);
            publishers[collided]?.publish(collider);
        }
    }
}
