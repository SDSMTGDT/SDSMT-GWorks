using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDSMTGDT.GWorks.Events.Experimental
{
    public interface CollisionRoute
    {
        bool activate(Collidable collider, Collidable collided);
        void run(Collidable collider, Collidable collided);
    }
}
