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
        /// <summary>
        /// Returns whether the route is valid
        /// </summary>
        /// <param name="collider">First collidable</param>
        /// <param name="collided">Second collidable</param>
        /// <returns></returns>
        bool activate(Collidable collider, Collidable collided);

        /// <summary>
        /// The action to take if the route is valid
        /// </summary>
        /// <param name="collider">First collidable</param>
        /// <param name="collided">Second collidable</param>
        void run(Collidable collider, Collidable collided);
    }
}
