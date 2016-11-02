using Microsoft.Xna.Framework;
using SDSMTGDT.GWorks.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDSMTGDT.GWorks.Physics.Collisions
{
    /// <summary>
    /// Contains the Collidable instances engaged in a collision
    /// </summary>
    public class CollisionEventInfo : GameEventInfo
    {
        /// <summary>
        /// The main collidable in the collision
        /// </summary>
        public Collidable Collider { get; }

        /// <summary>
        /// The other collidable in the collision
        /// </summary>
        public Collidable Collided { get; }

        /// <summary>
        /// The collisions manager which issued this info
        /// </summary>
        public CollisionManager PhysicsManager { get; }

        /// <summary>
        /// The collision group containing both collidables
        /// </summary>
        public CollisionGroup CollisionGroup { get; }

        /// <summary>
        /// Captures the rectangle of the collider at the time of the collision
        /// </summary>
        public Rectangle ColliderBounds { get; }

        /// <summary>
        /// Captures the rectangle of the collided at the time of the collision
        /// </summary>
        public Rectangle CollidedBounds { get; }
        
        /// <summary>
        /// Creates a CollisionEventInfo which holds two Collidables engaged in a collision
        /// </summary>
        /// <param name="collider">Collidable 1</param>
        /// <param name="collided">Collidable 2</param>
        public CollisionEventInfo(Collidable collider, Collidable collided, CollisionGroup group, CollisionManager manager)
        {
            Collider = collider;
            Collided = collided;
            CollisionGroup = group;
            PhysicsManager = manager;
            ColliderBounds = new Rectangle(collider.GetBounds().Location, collider.GetBounds().Size);
            CollidedBounds = new Rectangle(collided.GetBounds().Location, collided.GetBounds().Size);
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
