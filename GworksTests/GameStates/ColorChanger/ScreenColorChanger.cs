using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SDSMTGDT.GWorks.GameStates.ColorChanger
{
    /// <summary>
    /// The ScreenColorChanger is a test for drawing to the screen. The class
    /// creates a rectangle with a 1x1 texture the size of bounds. the color
    /// can then be changed with the draw logic.
    /// </summary>
    internal class ScreenColorChanger : DrawListener
    {
        private static Random rand = new Random();
        private static Texture2D rectTexture;
        private Color curr, dest;
        private double speed = .2;
        private Rectangle bounds;
        ///<summary>
        ///This is the constructor for our ScreenColorChanger graphics test.
        ///</summary>
        ///<param name="graphicsDevice">
        ///Graphics device is used to create a texture</param>
        ///<param name="bounds">
        ///Bounds defines where the color changer will draw</param>
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

        /// <summary>
        /// handles the drawing of our test.
        /// </summary>
        /// <param name="gameTime">This class tracks the time that the game has
        /// been running, as well as the time since the last check.</param>
        /// <param name="graphics">This class handles the drawing of a group
        /// of images in an efficient manner.It will handle drawing our
        /// colorChanger to the screen.</param>
        public void Draw(GameTime gameTime, SpriteBatch graphics)
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

        /// <summary>
        /// Returns the ZIndex of our color changer, it is set to int.MaxValue
        /// to ensure that it is drawn in the background.
        /// </summary>
        /// <returns>makes sure that the colorChanger is in the background
        /// </returns>
        public int GetZIndex()
        {
            return int.MaxValue;
        }
    }
}
