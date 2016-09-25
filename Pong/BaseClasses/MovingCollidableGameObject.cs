using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SDSMTGDT.GWorks.Physics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pong.BaseClasses
{
    class MovingCollidableGameObject : CollidableGameObject
    {
        public Vector2 velocity;

        public MovingCollidableGameObject(Rectangle bounds, CollisionManager collisions,
            Texture2D texture, Vector2 position, Vector2 startingVelocity)
            : base(bounds, collisions, texture, position)
        {
            velocity = startingVelocity;
        }

        public Vector2 getVelocity()
        {
            return velocity;
        }

        public void setVelocity(Vector2 velocity)
        {
            this.velocity = velocity;
        }

        public void setVelocity(int x, int y)
        {
            velocity.X = x;
            velocity.Y = y;
        }

        public void move (int milliseconds)
        {
            setPosition(velocity * milliseconds);
        }
    }
}
