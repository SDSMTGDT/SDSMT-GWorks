using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SDSMTGDT.GWorks.Physics;

namespace BrickBreaker.Helpers
{
    //pulls in collsion zones and gameobject
    abstract class CollidableGameObject : CollisionZone, GameObject
    {
        private Texture2D texture;
        private Vector2 location;

        internal CollidableGameObject(Rectangle bounds, Texture2D texture, PhysicsManager physics) :
            base(bounds, physics)
        {
            this.texture = texture;
            this.location = bounds.Location.ToVector2();
        }

        public Vector2 getLocation()
        {
            return location;
        }

        public Texture2D getTexture()
        {
            return texture;
        }

        public void setLocation(float x, float y)
        {
            location.X = x;
            location.Y = y;
            bounds.X = (int)Math.Round(x);
            bounds.Y = (int)Math.Round(y);
        }

        public void move(float x, float y)
        {
            location.X += x;
            location.Y += y;
            bounds.X = (int)Math.Round(location.X);
            bounds.Y = (int)Math.Round(location.Y);
        }
    }
}
