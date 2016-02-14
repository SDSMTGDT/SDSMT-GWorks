using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDSMTGDT.GWorks.Physics.Collisions.Routes
{
    /// <summary>
    /// Strategy pattern interface for handling collisions.
    /// It is implied that if activate returns true, the run method will be called
    /// </summary>
    public interface CollisionRoute
    {
        bool activate(Collidable collider, Collidable collided);
        void run(Collidable collider, Collidable collided);
    }
}
