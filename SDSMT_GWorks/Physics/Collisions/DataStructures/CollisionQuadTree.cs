using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDSMTGDT.GWorks.Physics.Collisions.DataStructures
{
    /// <summary>
    /// A Quad Tree implementation for finding collisions
    /// </summary>
    internal class CollisionQuadTree : CollisionStructure
    {
        /// <summary>
        /// The number of collidables to hold in each node. Disregarded for bottom nodes
        /// </summary>
        private int CAPACITY = 10;

        /// <summary>
        /// Self similar structures for each of this node's leaves
        /// </summary>
        private CollisionQuadTree northWest, northEast, southWest, southEast;

        /// <summary>
        /// The collidables held in this node
        /// </summary>
        private readonly List<Collidable> collidables;

        /// <summary>
        /// The boundary of this node in the collision quad tree. Must be a power of 2.
        /// </summary>
        private Rectangle boundary;

        /// <summary>
        /// Creates a new quad tree for colliding collidables
        /// </summary>
        /// <param name="x">X - Location of the quad tree with respect to the collidables</param>
        /// <param name="y">Y - Location of the quad tree with respect to the collidables</param>
        /// <param name="size">Size of the quad tree. Will be realigned to a power of 2</param>
        internal CollisionQuadTree(int x, int y, int size)
        {
            int roughGuessPow2 = (int)Math.Log(size, 2);
            int newSize = (int)Math.Pow(2, roughGuessPow2);
            if (newSize < size)
            {
                newSize = newSize * 2;
            }
            this.boundary = new Rectangle(x, y, newSize, newSize);

            //Allow bottom layer pooling
            if (size == 1)
            {
                CAPACITY = int.MaxValue;
            }

            collidables = new List<Collidable>(CAPACITY);
        }

        /// <summary>
        /// Insert a collidable into the quad tree.
        /// Implement the interface explicitly so that the method
        /// is only visible via CollisionStructure (which is internal).
        /// </summary>
        /// <param name="c">The collidable to insert</param>
        void CollisionStructure.Insert(Collidable c)
        {
            //If the collidable doesn't belong here, return
            if (!boundary.Intersects(c.GetBounds()))
            {
                return;
            }

            //If we have room insert the collidable
            if (collidables.Count < CAPACITY)
            {
                collidables.Add(c);
                return;
            }

            //If the children are uninitialized, initialize them.
            if (northWest == null)
            {
                Split();
            }

            //Find a spot for the collidable below
            ((CollisionStructure)northWest).Insert(c);
            ((CollisionStructure)northEast).Insert(c);
            ((CollisionStructure)southWest).Insert(c);
            ((CollisionStructure)southEast).Insert(c);
        }

        /// <summary>
        /// Remove a collidable from the quad tree.
        /// Implement the interface explicitly so that the method
        /// is only visible via CollisionStructure (which is internal).
        /// </summary>
        /// <param name="c">The collidable to remove</param>
        /// <returns></returns>
        bool CollisionStructure.Delete(Collidable c)
        {
            if(collidables.Remove(c))
            {
                return true;
            }

            //If the children are uninitialized, return false
            if (northWest != null)
            {
                return false;
            }

            //Start off false, if we successfully delete in any of our children, return true
            bool status = false;
            if ((northWest.boundary.Intersects(c.GetBounds())) && ((CollisionStructure)northWest).Delete(c))
                status = true;
            if ((northEast.boundary.Intersects(c.GetBounds())) && ((CollisionStructure)northEast).Delete(c))
                status = true;
            if ((southWest.boundary.Intersects(c.GetBounds())) && ((CollisionStructure)southWest).Delete(c))
                status = true;
            if ((southEast.boundary.Intersects(c.GetBounds())) && ((CollisionStructure)southEast).Delete(c))
                status = true;

            return status;
        }

        /// <summary>
        /// Updates the collision structure with a collidables new location and bounds
        /// </summary>
        /// <param name="c">The collidable to update</param>
        /// <returns>Returns true if the collidable was found, deleted, and reinserted</returns>
        bool CollisionStructure.Update(Collidable c)
        {
            bool exists = ((CollisionStructure)this).Delete(c);
            if (!exists)
                return false;
            ((CollisionStructure)this).Insert(c);
            return true;
        }

        /// <summary>
        /// Checks a given collidable against all other other collidables for collisions.
        /// Implement the interface explicitly so that the method
        /// is only visible via CollisionStructure (which is internal).
        /// </summary>
        /// <param name="c">The collidable to check</param>
        /// <returns>A unique list of intersecting colliables, 
        /// the passed collidable may be in this list</returns>
        IEnumerable<Collidable> CollisionStructure.CheckCollision(Collidable c)
        {
            // If the collidable isn't in this quad return null
            if (!boundary.Intersects(c.GetBounds()))
            {
                return null;
            }

            // Create a list of collidables to hold collisions
            List<Collidable> collisions = new List<Collidable>();

            // Test our collidables against c
            foreach (var other in collidables)
            {
                if (c != other && c.GetBounds().Intersects(other.GetBounds()))
                {
                    collisions.Add(other);
                }
            }

            if (northWest != null)
            {
                var possCollisions = ((CollisionStructure)northWest).CheckCollision(c);
                if (possCollisions != null)
                    collisions.AddRange(possCollisions);
            }
            if (northEast != null)
            {
                var possCollisions = ((CollisionStructure)northEast).CheckCollision(c);
                if (possCollisions != null)
                    collisions.AddRange(possCollisions);
            }
            if (southWest != null)
            {
                var possCollisions = ((CollisionStructure)southWest).CheckCollision(c);
                if (possCollisions != null)
                    collisions.AddRange(possCollisions);
            }
            if (southEast != null)
            {
                var possCollisions = ((CollisionStructure)southEast).CheckCollision(c);
                if (possCollisions != null)
                    collisions.AddRange(possCollisions);
            }
            
            return collisions.Distinct();
        }

        
        /// <summary>
        /// Splits the current quad into four more quads
        /// </summary>
        private void Split()
        {
            northWest = new CollisionQuadTree(
                boundary.X, boundary.Y,
                boundary.Width / 2
                );
            northEast = new CollisionQuadTree(
                boundary.X + boundary.Width / 2 + 1, boundary.Y,
                boundary.Width / 2
                );
            southWest = new CollisionQuadTree(
                boundary.X, boundary.Y + boundary.Height / 2 + 1,
                boundary.Width / 2
                );
            southEast = new CollisionQuadTree(
                boundary.X + boundary.Width / 2 + 1,
                boundary.Y + boundary.Height / 2 + 1,
                boundary.Width / 2
                );

        }
    }
}
