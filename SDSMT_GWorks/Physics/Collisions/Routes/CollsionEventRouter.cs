using SDSMTGDT.GWorks.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDSMTGDT.GWorks.Physics.Collisions.Routes
{
    /// <summary>
    /// Routes collision event information to the correct destination.
    /// Implements GameEventSubscriber such that it can hook directly into
    /// a CollisionEventPublisher.
    /// </summary>
    public class CollisionEventRouter : GameEventSubscriber<CollisionEventInfo>
    {
        /// <summary>
        /// A list of possible routes for collision info to be sent through
        /// </summary>
        private LinkedList<CollisionRoute> routes;

        /// <summary>
        /// Creates a new CollisionEventRouter
        /// </summary>
        public CollisionEventRouter()
        {
            this.routes = new LinkedList<CollisionRoute>();
        }

        /// <summary>
        /// Handles a CollisionEvent. Loops through each possible route, and sends
        /// the relevant information to the first valid route found
        /// </summary>
        /// <param name="source">Likely to be the collisions manager</param>
        /// <param name="eventInfo">CollisionEventInfo containing the Collidables</param>
        public void gameEventRecieved(object source, CollisionEventInfo eventInfo)
        {
            foreach (CollisionRoute route in routes)
            {
                if (route.activate(eventInfo))
                {
                    route.run(eventInfo);
                    return;
                }
            }
        }

        /// <summary>
        /// Adds a possible collision route to handle collision info
        /// </summary>
        /// <param name="route">A new collision route</param>
        public void addCollisionRoute(CollisionRoute route)
        {
            routes.AddLast(route);
        }

        /// <summary>
        /// Removes a collision route from the router
        /// </summary>
        /// <param name="route">An existing collision route</param>
        public void removeCollisionRoute(CollisionRoute route)
        {
            routes.Remove(route);
        }

    }
}
