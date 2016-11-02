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
        public T1 Collider { get; private set; }

        /// <summary>
        /// The other collidable in the collision
        /// </summary>
        public T2 Collided { get; private set; }

        /// <summary>
        /// The collisions manager which issued this info
        /// </summary>
        public CollisionManager PhysicsManager { get; private set; }

        /// <summary>
        /// The collision group containing both collidables
        /// </summary>
        public CollisionGroup CollisionGroup { get; private set; }

        /// <summary>
        /// Captures the rectangle of the collider at the time of the collision
        /// </summary>
        public Rectangle ColliderBounds { get; private set; }

        /// <summary>
        /// Captures the rectangle of the collided at the time of the collision
        /// </summary>
        public Rectangle CollidedBounds { get; private set; }

        public TypedCollisionEventInfo(CollisionEventInfo info)
        {
            Collider = (T1)info.Collider;
            Collided = (T2)info.Collided;
            PhysicsManager = info.PhysicsManager;
            CollisionGroup = info.CollisionGroup;
            ColliderBounds = new Rectangle(info.ColliderBounds.Location, info.ColliderBounds.Size);
            CollidedBounds = new Rectangle(info.CollidedBounds.Location, info.CollidedBounds.Size);
        }

        /// <summary>
        /// Returns the intersection of the two bounding boxes
        /// </summary>
        /// <returns>The intersection of the collidable's bounding boxes</returns>
        public Rectangle GetIntersection()
        {
            return Rectangle.Intersect(ColliderBounds, CollidedBounds);
        }

        /// <summary>
        /// Calculates the vector from the center of the collider to the center of the collided
        /// </summary>
        /// <returns>A vector from the center of the collider to the center of the collided</returns>
        public Vector2 GetCollisionVector()
        {
            return (CollidedBounds.Center - ColliderBounds.Center).ToVector2();
        }
    }
}
