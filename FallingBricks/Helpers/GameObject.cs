using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FallingBricks.Helpers
{
    abstract class GameObject : Movable, GraphicsObject
    {
        private Rectangle bounds;
        private Vector2 velocity;
        private Texture2D texture;

        protected GameObject()
        {
            this.bounds = new Rectangle();
            this.velocity = Vector2.Zero;
            this.texture = null;
        }

        public Rectangle getBounds()
        {
            return bounds;
        }

        protected void setBounds(Rectangle bounds)
        {
            this.bounds = bounds;
        }

        protected void setBounds(int x, int y, int width, int height)
        {
            this.bounds.X = x;
            this.bounds.Y = y;
            this.bounds.Width = width;
            this.bounds.Height = height;
        }

        public virtual Rectangle getDrawBounds()
        {
            return bounds;
        }

        public Point getLocation()
        {
            return bounds.Location;
        }

        public void setLocation(Point location)
        {
            bounds.Location = location;
        }

        public void setLocation(int x, int y)
        {
            bounds.X = x;
            bounds.Y = y;
        }

        public Texture2D getTexture()
        {
            return texture;
        }

        protected void setTexture(Texture2D texture)
        {
            this.texture = texture;
        }

        public void move(GameTime gameTime)
        {
            bounds.Location += (gameTime.ElapsedGameTime.Milliseconds * velocity).ToPoint();
        }

        public void setVelocity(Vector2 velocity)
        {
            this.velocity = velocity;
        }

        public void setVelocity(float x, float y)
        {
            velocity.X = x;
            velocity.Y = y;
        }
    }
}
