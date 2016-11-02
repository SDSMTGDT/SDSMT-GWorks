using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDSMTGDT.GWorks.Physics.Collisions.DataStructures
{
    /// <summary>
    /// Unifies collision structures such as collision lists, quad trees, r-trees. etc.
    /// Any collision backend may be used with the collision system as long as it exposes
    /// this interface.
    /// </summary>
    internal interface CollisionStructure
    {
        /// <summary>
        /// Insert a collidable into the structure
        /// </summary>
        /// <param name="c">The collidable to insert</param>
        void Insert(Collidable c);

        /// <summary>
        /// Remove a collidable from the structure
        /// </summary>
        /// <param name="c">The collidable to remove</param>
        /// <returns>True if the collidable was found and removed, else false</returns>
        bool Delete(Collidable c);

        /// <summary>
        /// Update a collidable in the structure
        /// </summary>
        /// <param name="c">The collidable to update</param>
        /// <returns>True if the collidable was found and updated, else false</returns>
        bool Update(Collidable c);

        /// <summary>
        /// Collides a collidable with the collidables in the structure.
        /// </summary>
        /// <param name="c">The collidable to check against</param>
        /// <returns>A enumerable collection containing the collidables 
        /// that intersect the passed in collidable</returns>
        IEnumerable<Collidable> CheckCollision(Collidable c);

    }
}
