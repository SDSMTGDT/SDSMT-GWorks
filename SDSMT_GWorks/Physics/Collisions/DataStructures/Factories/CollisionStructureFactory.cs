using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDSMTGDT.GWorks.Physics.Collisions.DataStructures.Factories
{
    public abstract class CollisionStructureFactory
    {
        internal abstract CollisionStructure createCollisionStructure();
    }
}
