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
        public Collidable collider { get; private set; }

        /// <summary>
        /// The other collidable in the collision
        /// </summary>
        public Collidable collided { get; private set; }

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
        
        /// <summary>
        /// Creates a CollisionEventInfo which holds two Collidables engaged in a collision
        /// </summary>
        /// <param name="collider">Collidable 1</param>
        /// <param name="collided">Collidable 2</param>
        public CollisionEventInfo(Collidable collider, Collidable collided, CollisionGroup group, PhysicsManager manager)
        {
            this.collider = collider;
            this.collided = collided;
            this.collisionGroup = group;
            this.physicsManager = manager;
            this.colliderBounds = new Rectangle(collider.getBounds().Location, collider.getBounds().Size);
            this.collidedBounds = new Rectangle(collided.getBounds().Location, collided.getBounds().Size);
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
