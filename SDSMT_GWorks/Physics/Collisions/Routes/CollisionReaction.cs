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
    public delegate void CollisionReaction(Collidable collider, Collidable collided);

    /// <summary>
    /// Function pointer for handling typed collisions
    /// </summary>
    /// <typeparam name="ColliderType">Type of the collider</typeparam>
    /// <typeparam name="CollidedType">Type of  the collided</typeparam>
    /// <param name="collider">First Collidable</param>
    /// <param name="collided">Second Collidable</param>
    public delegate void CollisionReaction<ColliderType, CollidedType>(ColliderType collider, CollidedType collided);
}
