using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SDSMTGDT.GWorks.Spriting
{
    public class Animation
    {
        private Texture2D source;

        /// <summary>
        /// List of rectangles that define sprites in a composite image.
        /// </summary>
        public IReadOnlyList<Rectangle> locations { get; private set; }

        /// <summary>
        /// List of the bounding boxes for each sprite in the animation. Allows
        /// for sprites that are larger or smaller than their frame bounds.
        /// </summary>
        public IReadOnlyList<Rectangle> bounds { get; private set; }

        /// <summary>
        /// How long each frame lasts. If a frame should be displayed for longer
        /// than another, add the frame multiple times.
        /// </summary>
        public TimeSpan frameDuration { get; private set; }

        /// <summary>
        /// Property that returns the number of frames in the animation based
        /// on the list of frames in the source image.
        /// </summary>
        public int frameCount {
            get
            {
                return locations.Count;
            }
        }

        /// <summary>
        /// Create a new animation with a set of frame locations and a source
        /// image.
        /// </summary>
        /// <param name="locations">Set of rectangles that define where in the
        /// source image the animation frame is located.</param>
        /// <param name="source">Source image containing all of the animation
        /// frames</param>
        public Animation(IReadOnlyList<Rectangle> locations, IReadOnlyList<Rectangle> bounds, Texture2D source, TimeSpan frameDuration)
        {
            this.locations = locations;
            this.bounds = bounds;
            this.source = source;
            this.frameDuration = frameDuration;
        }

        /// <summary>
        /// Draws the animated frame to the screen.
        /// </summary>
        /// <param name="batch">Opened sprite batch for drawing</param>
        /// <param name="dest">Rectangle to draw the frame in</param>
        /// <param name="i">Index of the frame to draw</param>
        public void draw(SpriteBatch batch, Rectangle dest, int i, 
            Vector2? origin = default(Vector2?), float rotation = 0, 
            Vector2? scale = default(Vector2?), Color? color = default(Color?), 
            SpriteEffects effects = SpriteEffects.None, float layerDepth = 0)
        {
            batch.Draw(source, null, dest, locations[i], origin, rotation, scale, color, effects, layerDepth);
        }
    }
}
