using System;
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
        private readonly Animation animation;
        private readonly bool looping;

        /// <summary>
        /// Returns whether or not the animation has played through
        /// </summary>
        public bool Finished
        {
            get
            {
                return !looping && accumulator.Ticks > animation.FrameDuration.Ticks * animation.FrameCount;
            }
        }

        /// <summary>
        /// Returns the current Bounds of this animation
        /// </summary>
        public Rectangle Bounds => animation.Bounds[frameNumber];

        /// <summary>
        /// Returns the height and width of the image
        /// </summary>
        public Vector2 ImageBounds => animation.Locations[frameNumber].Size.ToVector2();

        /// <summary>
        /// Fires when the bounding box of the animation changes
        /// </summary>
        public Action<Rectangle> BoundsChanged;

        /// <summary>
        /// Fires when the sprite advances to the next frame in the animation
        /// </summary>
        public Action FrameChanged;

        /// <summary>
        /// Fires when the animation finishes the last frame in the animation
        /// </summary>
        public Action AnimationFinished;

        public AnimationState(Animation animation, bool looping)
        {
            this.animation = animation;
            accumulator = TimeSpan.Zero;
            this.looping = looping;
            FrameChanged += () =>
            {
                if (Finished)
                    AnimationFinished();
            };
        }

        /// <summary>
        /// Resets the animation to its initial state.
        /// </summary>
        public void Reset()
        {
            frameNumber = 0;
            accumulator = TimeSpan.Zero;
        }

        /// <summary>
        /// Updates the animation with a given delta time. 
        /// It may advance the current frame.
        /// </summary>
        /// <param name="dt">The amount of time to advance</param>
        public void Update(GameTime dt)
        {
            accumulator += dt.ElapsedGameTime;
            if (!Finished)
            {
                int oldFrame = frameNumber;
                frameNumber = (int)(accumulator.Ticks / animation.FrameDuration.Ticks % animation.FrameCount);
                if (oldFrame != frameNumber)
                {
                    FrameChanged();
                    if (animation.Bounds[oldFrame] != animation.Bounds[frameNumber])
                        BoundsChanged(animation.Bounds[frameNumber]);
                }
            }
        }       

        public void Draw(SpriteBatch batch, Rectangle destination,
            Vector2? origin = default(Vector2?), float rotation = 0,
            Vector2? scale = default(Vector2?), Color? color = default(Color?),
            SpriteEffects effects = SpriteEffects.None, float layerDepth = 0)
        {
            animation.Draw(batch, destination, frameNumber, origin, rotation, scale, color, effects, layerDepth);
        }
    }
}
