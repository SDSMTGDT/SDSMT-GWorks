using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SDSMTGDT.GWorks.Physics;

namespace Pong.BaseClasses
{
    class CollidableGameObject : GameObjectCollision, GameObject
    {
        private Texture2D texture; /*the texture for the object*/
        private Vector2 velocity; /*the x and y velocities of the object*/
        private Vector2 position; /* the x and y position of the object*/
        private GraphicsDevice graphics; /*the graphics device the object uses*/

        public CollidableGameObject (Rectangle bounds, PhysicsManager physics,
            Texture2D texture, Vector2 startingVelocity, Vector2 position,
            GraphicsDevice graphics) : base(bounds, physics)
        {
            this.texture = texture;
            this.position = position;
            this.graphics = graphics;
            velocity = startingVelocity;
        }

        public Texture2D getTexture()
        {
            return texture;
        }

        public Vector2 getVelocity()
        {
            return velocity;
        }

        public Vector2 getPosition()
        {
            return position;
        }
    }
}
