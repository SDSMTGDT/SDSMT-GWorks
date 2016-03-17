using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDSMTGDT.GWorks.Physics.Collisions.DataStructures.Factories
{
    public class CollisionListFactory : CollisionStructureFactory
    {
        internal override CollisionStructure createCollisionStructure()
        {
            return new CollisionList();
        }
    }
}
