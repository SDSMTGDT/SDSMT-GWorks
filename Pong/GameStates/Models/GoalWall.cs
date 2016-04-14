using Pong.BaseClasses;
using SDSMTGDT.GWorks.Physics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pong.GameStates.Models
{
    class GoalWall : CollidableGameObject
    {

        public GoalWall (Rectangle bounds, PhysicsManager physics) 
            : base(bounds, physics, null, bounds.Location.ToVector2())
        {

        }
    }
}
