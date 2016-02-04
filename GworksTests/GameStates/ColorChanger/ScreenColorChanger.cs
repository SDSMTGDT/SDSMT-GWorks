using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SDSMTGDT.GWorks.GameStates.ColorChanger
{
    internal class ScreenColorChanger : DrawListener
    {
        private static Random rand = new Random();
        private static Texture2D rectTexture;
        private Color curr, dest;
        private double speed = .2;
        private Rectangle bounds;

        //Graphics device is used to create a texture
        //Bounds defines where the color changer will draw
        internal ScreenColorChanger(GraphicsDevice graphicsDevice, Rectangle bounds)
        {
            if (rectTexture == null)
            {
                rectTexture = new Texture2D(graphicsDevice, 1, 1, false, SurfaceFormat.Color);
                rectTexture.SetData(new Color[] { Color.White });
            }
            this.curr = new Color(rand.Next(256), rand.Next(256), rand.Next(256));
            this.dest = new Color(rand.Next(256), rand.Next(256), rand.Next(256));
            this.bounds = bounds;
        }

        public void draw(GameTime gameTime, SpriteBatch graphics)
        {
            graphics.Draw(rectTexture, bounds, curr);
            byte delta = (byte)Math.Round(gameTime.ElapsedGameTime.Milliseconds * speed);

            if (curr.R - dest.R < 0)
                curr.R += delta;
            else
                curr.R -= delta;

            if (curr.G - dest.G < 0)
                curr.G += delta;
            else
                curr.G -= delta;

            if (curr.B - dest.B < 0)
                curr.B += delta;
            else
                curr.B -= delta;

            if (Math.Abs(curr.R - dest.R) + Math.Abs(curr.G - dest.G) + Math.Abs(curr.B - dest.B) < 10)
            {
                dest.R = (byte)rand.Next(256);
                dest.G = (byte)rand.Next(256);
                dest.B = (byte)rand.Next(256);
            }
        }

        public int getZIndex()
        {
            return int.MaxValue;
        }
    }
}
