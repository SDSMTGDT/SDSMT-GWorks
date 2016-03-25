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
        private List<Collidable> collidables;

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

            this.collidables = new List<Collidable>(CAPACITY);
        }

        /// <summary>
        /// Insert a collidable into the quad tree.
        /// Implement the interface explicitly so that the method
        /// is only visible via CollisionStructure (which is internal).
        /// </summary>
        /// <param name="c">The collidable to insert</param>
        void CollisionStructure.insert(Collidable c)
        {
            //If the collidable doesn't belong here, return
            if (!boundary.Intersects(c.getBounds()))
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
                split();
            }

            //Find a spot for the collidable below
            ((CollisionStructure)northWest).insert(c);
            ((CollisionStructure)northEast).insert(c);
            ((CollisionStructure)southWest).insert(c);
            ((CollisionStructure)southEast).insert(c);
        }

        /// <summary>
        /// Remove a collidable from the quad tree.
        /// Implement the interface explicitly so that the method
        /// is only visible via CollisionStructure (which is internal).
        /// </summary>
        /// <param name="c">The collidable to remove</param>
        /// <returns></returns>
        bool CollisionStructure.delete(Collidable c)
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
            if ((northWest.boundary.Intersects(c.getBounds())) && ((CollisionStructure)northWest).delete(c))
                status = true;
            if ((northEast.boundary.Intersects(c.getBounds())) && ((CollisionStructure)northEast).delete(c))
                status = true;
            if ((southWest.boundary.Intersects(c.getBounds())) && ((CollisionStructure)southWest).delete(c))
                status = true;
            if ((southEast.boundary.Intersects(c.getBounds())) && ((CollisionStructure)southEast).delete(c))
                status = true;

            return status;
        }

        /// <summary>
        /// Checks a given collidable against all other other collidables for collisions.
        /// Implement the interface explicitly so that the method
        /// is only visible via CollisionStructure (which is internal).
        /// </summary>
        /// <param name="c">The collidable to check</param>
        /// <returns>A unique list of intersecting colliables, 
        /// the passed collidable may be in this list</returns>
        IEnumerable<Collidable> CollisionStructure.checkCollision(Collidable c)
        {
            // If the collidable isn't in this quad return null
            if (!boundary.Intersects(c.getBounds()))
            {
                return null;
            }

            // Create a list of collidables to hold collisions
            List<Collidable> collisions = new List<Collidable>();

            // Test our collidables against c
            foreach (var other in collidables)
            {
                if (c.getBounds().Intersects(other.getBounds()))
                {
                    collisions.Add(other);
                }
            }

            if (northWest != null)
            {
                var possCollisions = ((CollisionStructure)northWest).checkCollision(c);
                if (possCollisions != null)
                    collisions.AddRange(possCollisions);
            }
            if (northEast != null)
            {
                var possCollisions = ((CollisionStructure)northEast).checkCollision(c);
                if (possCollisions != null)
                    collisions.AddRange(possCollisions);
            }
            if (southWest != null)
            {
                var possCollisions = ((CollisionStructure)southWest).checkCollision(c);
                if (possCollisions != null)
                    collisions.AddRange(possCollisions);
            }
            if (southEast != null)
            {
                var possCollisions = ((CollisionStructure)southEast).checkCollision(c);
                if (possCollisions != null)
                    collisions.AddRange(possCollisions);
            }
            
            return collisions.Distinct();
        }

        
        /// <summary>
        /// Splits the current quad into four more quads
        /// </summary>
        private void split()
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
