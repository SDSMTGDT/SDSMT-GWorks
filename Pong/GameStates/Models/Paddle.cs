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
    class Paddle : MovingCollidableGameObject
    {

        public Paddle (Rectangle bounds, CollisionManager collisions,
            GraphicsDevice graphics, Vector2 position ) :base( bounds, collisions,
                createPaddleTexture(graphics, bounds), position, new Vector2(0,0))
        {

        }

        public static Texture2D createPaddleTexture (GraphicsDevice graphics,
            Rectangle bounds)
        {
            return new Texture2D(graphics, bounds.Width, bounds.Height);
        }
    }
}
