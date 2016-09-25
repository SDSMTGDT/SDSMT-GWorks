using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDSMTGDT.GWorks.Physics.Collisions.DataStructures.Factories
{
    /// <summary>
    /// Encapsulates the information needed to create a CollisionList
    /// Used by collisions manager to create list backed collision groups
    /// </summary>
    public class CollisionListFactory : CollisionStructureFactory
    {
        internal override CollisionStructure createCollisionStructure()
        {
            return new CollisionList();
        }
    }
}
