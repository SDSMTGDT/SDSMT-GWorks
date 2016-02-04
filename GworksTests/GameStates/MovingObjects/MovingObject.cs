using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SDSMTGDT.GWorks.GameStates.MovingObjects
{
    public class MovingObject : DrawListener, UpdateListener
    {
        private static Texture2D movingTexture;
        private Rectangle bounds;
        private Color movingColor;
        private Viewport port;
        private bool direction;

        public MovingObject(GraphicsDevice graphicsDevice, Rectangle bounds)
        {
            if ( movingTexture == null)
            {
                movingTexture = new Texture2D(graphicsDevice,1,1,false,SurfaceFormat.Color);
                movingTexture.SetData(new Color[] { Color.White });
            }
            this.bounds = bounds;
            this.movingColor = new Color(255,127,127);
            this.port = graphicsDevice.Viewport;
            this.direction = true;
        }

        public void draw(GameTime gameTime, SpriteBatch graphics)
        {
            graphics.Draw(movingTexture, bounds, movingColor);
        }

        public int getZIndex()
        {
            return int.MaxValue / 2;
        }

        public void update(GameTime gameTime)
        {
            if ((bounds.Y + bounds.Height) > (port.Height - 20))
                direction = !direction;
            else if (bounds.Y < 20)
                direction = true;
            if (direction)
            {
                bounds.Y += (1 * gameTime.ElapsedGameTime.Milliseconds);
            }
            else
            {
                bounds.Y -= (1 * gameTime.ElapsedGameTime.Milliseconds);
            }
        }
    }
}
