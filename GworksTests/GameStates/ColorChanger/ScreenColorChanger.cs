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
        private Random rand = new Random();
        private Color curr = new Color(0, 0, 0);
        private Color dest = new Color(255, 255, 255);
        private double speed = 0.2;
        public void draw(GameTime gameTime, SpriteBatch graphics)
        {
            graphics.GraphicsDevice.Clear(curr);
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
            return 0;
        }
    }
}
