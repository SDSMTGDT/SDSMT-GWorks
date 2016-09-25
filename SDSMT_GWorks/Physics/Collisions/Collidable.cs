using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDSMTGDT.GWorks.Physics.Collisions
{
    /// <summary>
    /// Marks an object as collidable.
    /// Collidable objects must expose a axis aligned bounding box.
    /// </summary>
    public interface Collidable
    {
        /// <summary>
        /// An axis aligned bounding box is needed to provide
        /// quick efficient collisions
        /// </summary>
        /// <returns>The axis aligned bounding box of the Collidable object</returns>
        Rectangle getBounds();

        CollisionEventPublisher collisionPublisher { get; }
    }
}
