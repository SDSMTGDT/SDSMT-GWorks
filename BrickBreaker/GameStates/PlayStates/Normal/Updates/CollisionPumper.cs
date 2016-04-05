using SDSMTGDT.GWorks.GameStates;
using SDSMTGDT.GWorks.Physics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace BrickBreaker.GameStates.PlayStates.Normal.Updates
{
    class CollisionPumper : UpdateListener
    {
        private PhysicsManager physics;
        
        internal CollisionPumper(PhysicsManager physics)
        {
            this.physics = physics;
        }

        public void update(GameTime gameTime)
        {
            physics.checkAllCollisions();
        }
    }
}
