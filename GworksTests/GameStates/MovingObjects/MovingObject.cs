using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SDSMTGDT.GWorks.GameStates.MovingObjects
{
    /// <summary>
    /// This class creates a test moving object and updates its position based
    /// on its location on the screen.
    /// </summary>
    public class MovingObject : DrawListener, UpdateListener
    {
        private static Texture2D movingTexture;
        private Rectangle bounds;
        private Color movingColor;
        private Viewport port;
        private bool direction;

        /// <summary>
        /// Constructor for a movingObject. This object is used to test our
        /// update and draw code simultaneously.
        /// </summary>
        /// <param name="graphicsDevice">A class containing information
        /// concerning how to draw to the screen.</param>
        /// <param name="bounds">A rectangle object that will define the size
        /// of the moving object.</param>
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

        /// <summary>
        /// This method handles drawing the moving object with the help
        /// of the SpriteBatch.
        /// </summary>
        /// <param name="gameTime">A class that tracks the time since the
        /// game began, and since the last check against the time was made.
        /// Required by the base class' definition of the draw function.</param>
        /// <param name="graphics">This class handles the drawing of a group
        /// of images in an efficient manner. It will handle drawing our
        /// movingObject to the screen.</param>
        public void Draw(GameTime gameTime, SpriteBatch graphics)
        {
            graphics.Draw(movingTexture, bounds, movingColor);
        }

        /// <summary>
        /// Returns the depth of our movingObject. The depth is set to half of
        /// the maximum depth.
        /// </summary>
        /// <returns>the maximum depth of our object divided by two. Used to
        /// allow the moving Object to draw over the background.</returns>
        public int GetZIndex()
        {
            return int.MaxValue / 2;
        }

        /// <summary>
        /// This method handles updating the movingObject based on its current
        /// position.
        /// </summary>
        /// <param name="gameTime">A class that tracks the time since the
        /// game began, and since the last check against the time was made.
        /// Used to determine how far the movingObject has moved since the
        /// last check.</param>
        public void Update(GameTime gameTime)
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
