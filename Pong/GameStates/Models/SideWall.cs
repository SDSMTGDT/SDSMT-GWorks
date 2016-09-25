using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pong.BaseClasses;
using SDSMTGDT.GWorks.Physics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pong.GameStates.Models
{
    class SideWall : CollidableGameObject
    {

        public SideWall (Rectangle bounds, CollisionManager collisions,
            GraphicsDevice graphics) :base(bounds, collisions, 
                createSideWallTexture(graphics, bounds), bounds.Location.ToVector2())
        {

        }

        private static Texture2D createSideWallTexture (GraphicsDevice graphics,
            Rectangle bounds)
        {
            return new Texture2D(graphics, bounds.Width, bounds.Height);
        }
    }
}
