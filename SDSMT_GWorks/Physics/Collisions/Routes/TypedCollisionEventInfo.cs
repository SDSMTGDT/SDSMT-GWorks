using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDSMTGDT.GWorks.Physics.Collisions.Routes
{
    /// <summary>
    /// Typed version of the CollisionEventInfo class. 
    /// Used to convert collidables to their types in an info object.
    /// </summary>
    /// <typeparam name="T1">Type of the collider</typeparam>
    /// <typeparam name="T2">Type of the collided</typeparam>
    public class TypedCollisionEventInfo<T1, T2> where T1 : Collidable where T2 : Collidable
    {
        /// <summary>
        /// The main collidable in the collision
        /// </summary>
        public T1 collider { get; private set; }

        /// <summary>
        /// The other collidable in the collision
        /// </summary>
        public T2 collided { get; private set; }

        /// <summary>
        /// The physics manager which issued this info
        /// </summary>
        public PhysicsManager physicsManager { get; private set; }

        /// <summary>
        /// The collision group containing both collidables
        /// </summary>
        public CollisionGroup collisionGroup { get; private set; }

        /// <summary>
        /// Captures the rectangle of the collider at the time of the collision
        /// </summary>
        public Rectangle colliderBounds { get; private set; }

        /// <summary>
        /// Captures the rectangle of the collided at the time of the collision
        /// </summary>
        public Rectangle collidedBounds { get; private set; }

        public TypedCollisionEventInfo(CollisionEventInfo info)
        {
            this.collider = (T1)info.collider;
            this.collided = (T2)info.collided;
            this.physicsManager = info.physicsManager;
            this.collisionGroup = info.collisionGroup;
            this.colliderBounds = new Rectangle(info.colliderBounds.Location, info.colliderBounds.Size);
            this.collidedBounds = new Rectangle(info.collidedBounds.Location, info.collidedBounds.Size);
        }

        /// <summary>
        /// Returns the intersection of the two bounding boxes
        /// </summary>
        /// <returns>The intersection of the collidable's bounding boxes</returns>
        public Rectangle getIntersection()
        {
            return Rectangle.Intersect(colliderBounds, collidedBounds);
        }

        /// <summary>
        /// Calculates the vector from the center of the collider to the center of the intersection
        /// </summary>
        /// <returns>A vector from the center of the collider to the center of the intersection</returns>
        public Vector2 getCollisionVector()
        {
            return (getIntersection().Center - colliderBounds.Center).ToVector2();
        }
    }
}
