using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDSMTGDT.GWorks.Physics.Collisions.Routes
{
    public class TypeCollisionRoute<TypeCheck> : CollisionRoute
    {
        private CollisionReaction reaction;

        public TypeCollisionRoute(CollisionReaction reaction)
        {
            this.reaction = reaction;
        }

        public bool activate(Collidable collider, Collidable collided)
        {
            return collided is TypeCheck;
        }

        public void run(Collidable collider, Collidable collided)
        {
            reaction(collider, collider);
        }
    }
}
