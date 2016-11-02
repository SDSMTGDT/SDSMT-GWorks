using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDSMTGDT.GWorks.Physics.Collisions.Routes
{
    /// <summary>
    /// Strategy pattern interface for handling collisions.
    /// It is implied that if Activate returns true, the run method will be called
    /// </summary>
    public interface CollisionRoute
    {
        /// <summary>
        /// Returns whether the route is valid
        /// </summary>
        /// <param name="info">Collision info</param>
        /// <returns>True if this route should handle the info</returns>
        bool Activate(CollisionEventInfo info);

        /// <summary>
        /// The action to take if the route is valid
        /// </summary>
        /// <param name="info">Collision info</param>
        void Run(CollisionEventInfo info);
    }
}
