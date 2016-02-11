using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDSMTGDT.GWorks.Events.Experimental
{
    public class CollisionEventRouter : GameEventSubscriber<CollisionEventInfo>
    {
        private List<CollisionRoute> routes;
        public CollisionEventRouter()
        {
            this.routes = new List<CollisionRoute>();
        }

        public void gameEventRecieved(object source, CollisionEventInfo eventInfo)
        {
            foreach (CollisionRoute route in routes)
            {
                if (route.activate(eventInfo.collider, eventInfo.collided))
                {
                    route.run(eventInfo.collider, eventInfo.collided);
                    return;
                }
            }
        }

        public void addCollisionRoute(CollisionRoute route)
        {
            routes.Add(route);
        }

        public void removeCollisionRoute(CollisionRoute route)
        {
            routes.Remove(route);
        }

    }
}
