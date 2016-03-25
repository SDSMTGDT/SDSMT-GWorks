using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDSMTGDT.GWorks.Physics.Collisions.DataStructures.Factories
{
    /// <summary>
    /// Used to select which collision structure
    /// should be used to back a collision group
    /// </summary>
    public abstract class CollisionStructureFactory
    {
        /// <summary>
        /// Override this method to create a new collision structure.
        /// This method will be called by the physics manager when
        /// creating new collision groups.
        /// </summary>
        /// <returns>A new Collision Structure</returns>
        internal abstract CollisionStructure createCollisionStructure();
    }
}
