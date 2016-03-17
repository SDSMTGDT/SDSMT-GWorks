using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDSMTGDT.GWorks.Physics.Collisions.DataStructures
{
    internal class CollisionList : CollisionStructure
    {
        private LinkedList<Collidable> collidables;

        internal CollisionList()
        {
            collidables = new LinkedList<Collidable>();
        }

        IEnumerable<Collidable> CollisionStructure.checkCollision(Collidable c)
        {
            LinkedList<Collidable> collisions = new LinkedList<Collidable>();
            foreach (Collidable other in collidables)
            {
                if (other.getBounds().Intersects(c.getBounds()))
                {
                    collisions.AddLast(other);
                }
            }
            return collisions;
        }

        bool CollisionStructure.delete(Collidable c)
        {
            return collidables.Remove(c);
        }

        void CollisionStructure.insert(Collidable c)
        {
            collidables.AddLast(c);
        }
    }
}
