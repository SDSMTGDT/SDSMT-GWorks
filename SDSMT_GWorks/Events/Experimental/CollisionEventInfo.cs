using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDSMTGDT.GWorks.Events.Experimental
{
    public class CollisionEventInfo : GameEventInfo
    {
        public Collidable collider { get; private set; }
        public Collidable collided { get; private set; }
        public CollisionEventInfo(Collidable collider, Collidable collided)
        {
            this.collider = collider;
            this.collided = collided;
        }
    }
}
