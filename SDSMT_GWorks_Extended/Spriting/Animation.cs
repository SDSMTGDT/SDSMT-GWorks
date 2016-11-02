using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SDSMTGDT.GWorks.Spriting
{
    public class Animation
    {
        private readonly Texture2D source;

        /// <summary>
        /// List of rectangles that define sprites in a composite image.
        /// </summary>
        public IReadOnlyList<Rectangle> Locations { get; }

        /// <summary>
        /// List of the bounding boxes for each sprite in the animation. Allows
        /// for sprites that are larger or smaller than their frame Bounds.
        /// </summary>
        public IReadOnlyList<Rectangle> Bounds { get; private set; }

        /// <summary>
        /// How long each frame lasts. If a frame should be displayed for longer
        /// than another, add the frame multiple times.
        /// </summary>
        public TimeSpan FrameDuration { get; private set; }

        /// <summary>
        /// Property that returns the number of frames in the animation based
        /// on the list of frames in the source image.
        /// </summary>
        public int FrameCount => Locations.Count;

        /// <summary>
        /// Create a new animation with a set of frame locations and a source
        /// image.
        /// </summary>
        /// <param name="locations">Set of rectangles that define where in the
        /// source image the animation frame is located.</param>
        /// <param name="bounds">Set of rectangles that define the collision Bounds of each frame</param>
        /// <param name="source">Source image containing all of the animation
        /// frames</param>
        /// <param name="frameDuration">How long each frame should last</param>
        public Animation(IReadOnlyList<Rectangle> locations, IReadOnlyList<Rectangle> bounds, Texture2D source, TimeSpan frameDuration)
        {
            this.source = source;
            Locations = locations;
            Bounds = bounds;
            FrameDuration = frameDuration;
        }

        /// <summary>
        /// Draws the animated frame to the screen.
        /// </summary>
        /// <param name="batch">Opened sprite batch for drawing</param>
        /// <param name="dest">Rectangle to draw the frame in</param>
        /// <param name="i">Index of the frame to draw</param>
        public void Draw(SpriteBatch batch, Rectangle dest, int i, 
            Vector2? origin = default(Vector2?), float rotation = 0, 
            Vector2? scale = default(Vector2?), Color? color = default(Color?), 
            SpriteEffects effects = SpriteEffects.None, float layerDepth = 0)
        {
            batch.Draw(source, null, dest, Locations[i], origin, rotation, scale, color, effects, layerDepth);
        }
    }
}
