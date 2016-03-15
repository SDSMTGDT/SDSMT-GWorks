using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDSMTGDT.GWorks.Physics.Collisions
{
    public class CollisionQuadTree
    {
        private const int CAPACITY = 10;

        private CollisionQuadTree northWest, northEast, southWest, southEast;
        private List<Collidable> collidables;
        private Rectangle boundary;

        public CollisionQuadTree(int x, int y, int size)
        {
            int roughGuessPow2 = (int)Math.Log(size, 2);
            int newSize = (int)Math.Pow(2, roughGuessPow2);
            if (newSize < size)
            {
                newSize = newSize * 2;
            }
            this.boundary = new Rectangle(x, y, newSize, newSize);
            this.collidables = new List<Collidable>(CAPACITY);
        }

        public void insert(Collidable c)
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
            
            northWest.insert(c);
            northEast.insert(c);
            southWest.insert(c);
            southEast.insert(c);
        }

        public bool delete(Collidable c)
        {
            if(collidables.Remove(c))
            {
                return true;
            }
            return northWest != null && (
                   (northWest.boundary.Intersects(c.getBounds()) && northWest.delete(c)) || 
                   (northEast.boundary.Intersects(c.getBounds()) && northEast.delete(c)) ||
                   (southWest.boundary.Intersects(c.getBounds()) && southWest.delete(c)) || 
                   (southEast.boundary.Intersects(c.getBounds()) && southEast.delete(c)));
            
        }

        public IEnumerable<Collidable> checkCollision(Collidable c)
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
                collisions.AddRange(northWest.checkCollision(c));
            }
            if (northEast != null)
            {
                collisions.AddRange(northEast.checkCollision(c));
            }
            if (southWest != null)
            {
                collisions.AddRange(southWest.checkCollision(c));
            }
            if (southEast != null)
            {
                collisions.AddRange(southEast.checkCollision(c));
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
