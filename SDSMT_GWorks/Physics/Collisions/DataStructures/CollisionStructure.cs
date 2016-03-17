using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDSMTGDT.GWorks.Physics.Collisions.DataStructures
{
    internal interface CollisionStructure
    {
        void insert(Collidable c);
        bool delete(Collidable c);
        IEnumerable<Collidable> checkCollision(Collidable c);

    }
}
