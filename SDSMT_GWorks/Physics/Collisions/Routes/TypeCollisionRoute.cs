using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDSMTGDT.GWorks.Physics.Collisions.Routes
{
    /// <summary>
    /// Routes collision event information to the appropriate
    /// function based on the type of the second Collidable (the collided)
    /// in the event.
    /// </summary>
    /// <typeparam name="CollidedType">The type to check the first Collidable against</typeparam>
    /// <typeparam name="CollidedType">The type to check the second Collidable against</typeparam>
    public class TypeCollisionRoute<ColliderType, CollidedType> : CollisionRoute
    {
        /// <summary>
        /// Function to call when the second Collidable is of type passed in 
        /// to TypeCollisionRouter
        /// </summary>
        private CollisionReaction<ColliderType, CollidedType> reaction;

        /// <summary>
        /// Constructs a TypeCollisionRoute with the given function to call
        /// when the check passes
        /// </summary>
        /// <param name="reaction">Delegate to function to becalled when the check passes</param>
        public TypeCollisionRoute(CollisionReaction<ColliderType, CollidedType> reaction)
        {
            this.reaction = reaction;
        }

        /// <summary>
        /// Checks the second Collidable (collided) against the Type specified in
        /// the declaration of this class. Returns true if the types match
        /// </summary>
        /// <param name="collider">First Collidable</param>
        /// <param name="collided">Second Collidable</param>
        /// <returns></returns>
        public bool activate(Collidable collider, Collidable collided)
        {
            return collider is ColliderType && collided is CollidedType;
        }

        /// <summary>
        /// Passes the Collidable instances to the reaction function
        /// this instance was initialized with
        /// </summary>
        /// <param name="collider">First Collidable</param>
        /// <param name="collided">Second Collidable</param>
        public void run(Collidable collider, Collidable collided)
        {
            reaction((ColliderType)collider, (CollidedType)collider);
        }
    }
}
