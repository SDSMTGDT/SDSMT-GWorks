using BrickBreaker.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using SDSMTGDT.GWorks.Physics;
using SDSMTGDT.GWorks.Physics.Collisions;

namespace BrickBreaker.GameStates.PlayStates.Normal.Model
{
    interface Wall : Collidable
    { }

    class NorthWall : CollisionZone, Wall
    {
        internal NorthWall(Rectangle screen, PhysicsManager physics) : 
            base(new Rectangle(screen.X - 1, screen.Y - 1, screen.Width + 1, 1), physics)
        {
        }
    }

    class SouthWall : CollisionZone, Wall
    {
        internal SouthWall(Rectangle screen, PhysicsManager physics) : 
            base(new Rectangle(screen.X - 1, screen.Y + screen.Height, screen.Width + 1, 1), physics)
        {
        }
    }

    class EastWall : CollisionZone, Wall
    {
        internal EastWall(Rectangle screen, PhysicsManager physics) : 
            base(new Rectangle(screen.X + screen.Width, screen.Y, 1, screen.Height), physics)
        {
        }
    }

    class WestWall : CollisionZone, Wall
    {
        internal WestWall(Rectangle screen, PhysicsManager physics) : 
            base(new Rectangle(screen.X - 1, screen.Y, 1, screen.Height), physics)
        {
        }
    }
}
