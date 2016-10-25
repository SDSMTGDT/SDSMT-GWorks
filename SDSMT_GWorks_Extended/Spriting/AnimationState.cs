using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SDSMTGDT.GWorks.Spriting
{
    public class AnimationState : Sprite
    {
        /// <summary>
        /// Time the animation has been running. 
        /// Long running animations are safe.
        /// </summary>
        private TimeSpan accumulator;
        private int frameNumber;
        private Animation animation;
        private bool looping;

        /// <summary>
        /// Returns whether or not the animation has played through
        /// </summary>
        public bool finished
        {
            get
            {
                return !looping && accumulator.Ticks > animation.frameDuration.Ticks * animation.frameCount;
            }
        }

        /// <summary>
        /// Returns the current bounds of this animation
        /// </summary>
        public Rectangle bounds
        {
            get
            {
                return animation.bounds[frameNumber];
            }
        }

        /// <summary>
        /// Returns the height and width of the image
        /// </summary>
        public Vector2 imageBounds
        {
            get
            {
                return animation.locations[frameNumber].Size.ToVector2();
            }
        }

        /// <summary>
        /// Fires when the bounding box of the animation changes
        /// </summary>
        public event Action<Rectangle> boundsChanged;

        /// <summary>
        /// Fires when the sprite advances to the next frame in the animation
        /// </summary>
        public event Action frameChanged;

        /// <summary>
        /// Fires when the animation finishes the last frame in the animation
        /// </summary>
        public event Action animationFinished;

        AnimationState(Animation animation, bool looping)
        {
            this.animation = animation;
            this.accumulator = TimeSpan.Zero;
            this.looping = looping;
            frameChanged += () =>
            {
                if (finished)
                    animationFinished();
            };
        }

        /// <summary>
        /// Resets the animation to its initial state.
        /// </summary>
        public void reset()
        {
            this.frameNumber = 0;
            this.accumulator = TimeSpan.Zero;
        }

        /// <summary>
        /// Updates the animation with a given delta time. 
        /// It may advance the current frame.
        /// </summary>
        /// <param name="dt">The amount of time to advance</param>
        public void update(GameTime dt)
        {
            accumulator += dt.ElapsedGameTime;
            if (!finished)
            {
                int oldFrame = frameNumber;
                frameNumber = (int)(accumulator.Ticks / animation.frameDuration.Ticks % animation.frameCount);
                if (oldFrame != frameNumber)
                {
                    frameChanged();
                    if (animation.bounds[oldFrame] != animation.bounds[frameNumber])
                        boundsChanged(animation.bounds[frameNumber]);
                }
            }
        }       

        public void draw(SpriteBatch batch, Rectangle destination,
            Vector2? origin = default(Vector2?), float rotation = 0,
            Vector2? scale = default(Vector2?), Color? color = default(Color?),
            SpriteEffects effects = SpriteEffects.None, float layerDepth = 0)
        {
            animation.draw(batch, destination, frameNumber, origin, rotation, scale, color, effects, layerDepth);
        }
    }
}
