using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDSMTGDT.GWorks.Physics.Collisions.DataStructures
{
    /// <summary>
    /// Reference implementation of a collision structure using a linked list
    /// </summary>
    internal class CollisionList : CollisionStructure
    {
        /// <summary>
        /// Hold the collidables in a linked list for efficiency rather than an array
        /// </summary>
        private LinkedList<Collidable> collidables;

        /// <summary>
        /// Create a new collision list
        /// </summary>
        internal CollisionList()
        {
            collidables = new LinkedList<Collidable>();
        }

        /// <summary>
        /// Checks a passed collidable against all other collidables.
        /// If the passed collidable is in the structure, it will be included.
        /// Implement the interface explicitly so that the method
        /// is only visible via CollisionStructure (which is internal).
        /// </summary>
        /// <param name="c">The passed collidable</param>
        /// <returns>An enumerable collection of the intersecting collidables</returns>
        IEnumerable<Collidable> CollisionStructure.CheckCollision(Collidable c)
        {
            LinkedList<Collidable> collisions = new LinkedList<Collidable>();
            foreach (Collidable other in collidables)
            {
                if (c != other && other.GetBounds().Intersects(c.GetBounds()))
                {
                    collisions.AddLast(other);
                }
            }
            return collisions;
        }

        /// <summary>
        /// Removes a passed collidable from the list
        /// </summary>
        /// <param name="c">The passed collidable</param>
        /// <returns>True if the collidable was found and removed, else false</returns>
        bool CollisionStructure.Delete(Collidable c)
        {
            return collidables.Remove(c);
        }

        /// <summary>
        /// Meant to update a collidable if the structure relies on location info
        /// </summary>
        /// <param name="c">The collidable to update</param>
        /// <returns>Returns true if the structure contains the collidable</returns>
        bool CollisionStructure.Update(Collidable c)
        {
            return collidables.Contains(c);
        }

        /// <summary>
        /// Adds the passed collidable to the list
        /// </summary>
        /// <param name="c">The passed collidable</param>
        void CollisionStructure.Insert(Collidable c)
        {
            collidables.AddLast(c);
        }
    }
}
