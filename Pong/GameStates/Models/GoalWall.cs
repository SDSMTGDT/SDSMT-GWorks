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

        public GoalWall (Rectangle bounds, CollisionManager collisions) 
            : base(bounds, collisions, null, bounds.Location.ToVector2())
        {

        }
    }
}
