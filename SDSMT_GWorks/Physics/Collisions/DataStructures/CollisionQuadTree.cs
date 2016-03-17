using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDSMTGDT.GWorks.Physics.Collisions.DataStructures
{
    internal class CollisionQuadTree : CollisionStructure
    {
        private int CAPACITY = 10;

        private CollisionQuadTree northWest, northEast, southWest, southEast;
        private List<Collidable> collidables;
        private Rectangle boundary;

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

        void CollisionStructure.insert(Collidable c)
        {
            if (!boundary.Intersects(c.getBounds()))
            {
                return;
            }

            if (collidables.Count < CAPACITY)
            {
                collidables.Add(c);
                return;
            }

            if (northWest == null)
            {
                split();
            }

            ((CollisionStructure)northWest).insert(c);
            ((CollisionStructure)northEast).insert(c);
            ((CollisionStructure)southWest).insert(c);
            ((CollisionStructure)southEast).insert(c);
        }

        bool CollisionStructure.delete(Collidable c)
        {
            if(collidables.Remove(c))
            {
                return true;
            }
            return northWest != null && (
                   (northWest.boundary.Intersects(c.getBounds()) && ((CollisionStructure)northWest).delete(c)) || 
                   (northEast.boundary.Intersects(c.getBounds()) && ((CollisionStructure)northEast).delete(c)) ||
                   (southWest.boundary.Intersects(c.getBounds()) && ((CollisionStructure)southWest).delete(c)) || 
                   (southEast.boundary.Intersects(c.getBounds()) && ((CollisionStructure)southEast).delete(c)));
            
        }

        IEnumerable<Collidable> CollisionStructure.checkCollision(Collidable c)
        {
            List<Collidable> collisions = new List<Collidable>();
            if (!boundary.Intersects(c.getBounds()))
            {
                return collisions;
            }
            foreach(var other in collidables)
            {
                if (c.getBounds().Intersects(other.getBounds()))
                {
                    collisions.Add(other);
                }
            }
            if (northWest != null)
            {
                collisions.AddRange(((CollisionStructure)northWest).checkCollision(c));
            }
            if (northEast != null)
            {
                collisions.AddRange(((CollisionStructure)northEast).checkCollision(c));
            }
            if (southWest != null)
            {
                collisions.AddRange(((CollisionStructure)southWest).checkCollision(c));
            }
            if (southEast != null)
            {
                collisions.AddRange(((CollisionStructure)southEast).checkCollision(c));
            }
            
            return collisions.Distinct();
        }

        //might be off by one
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
