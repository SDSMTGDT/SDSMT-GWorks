using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SDSMTGDT.GWorks.Physics;

namespace Pong.BaseClasses
{
    class CollidableGameObject : CollisionZone, GameObject
    {
        protected Texture2D texture; /*the texture for the object*/
        protected Vector2 position; /* the x and y position of the object*/

        public CollidableGameObject (Rectangle bounds, CollisionManager collisions,
            Texture2D texture, Vector2 position) : base(bounds, collisions)
        {
            this.texture = texture;
            this.position = position;
        }

        public Texture2D getTexture()
        {
            return texture;
        }

        public void setTexture(Texture2D texture)
        {
            this.texture = texture;
        }

        public Vector2 getPosition()
        {
            return position;
        }

        public void setPosition(Vector2 position)
        {
            this.position = position;
            bounds.Location = position.ToPoint();
        }

        public void setPosition (int x, int y)
        {
            position.X = x;
            position.Y = y;
            bounds.X = x;
            bounds.Y = y;
        }
    }
}
