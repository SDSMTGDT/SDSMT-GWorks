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
        public Collidable collider { get; private set; }
        public Collidable collided { get; private set; }
        /// <summary>
        /// Creates a CollisionEventInfo which holds two Collidables engaged in a collision
        /// </summary>
        /// <param name="collider">Collidable 1</param>
        /// <param name="collided">Collidable 2</param>
        public CollisionEventInfo(Collidable collider, Collidable collided)
        {
            this.collider = collider;
            this.collided = collided;
        }
    }
}
