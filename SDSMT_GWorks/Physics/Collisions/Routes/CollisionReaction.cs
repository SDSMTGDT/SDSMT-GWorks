using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDSMTGDT.GWorks.Physics.Collisions.Routes
{
    /// <summary>
    /// Function pointer for handling collisions
    /// </summary>
    /// <param name="collider">First Collidable</param>
    /// <param name="collided">Second Collidable</param>
    public delegate void CollisionReaction<ColliderType, CollidedType>(TypedCollisionEventInfo<ColliderType, CollidedType> info) 
        where ColliderType : Collidable where CollidedType : Collidable;
}
