using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SDSMTGDT.GWorks.Spriting
{
    public class AnimationState
    {
        /// <summary>
        /// Time the animation has been running. 
        /// Long running animations are safe.
        /// </summary>
        private TimeSpan accumulator;
        private int frameNumber;
        private Animation animation;
        private bool looping;

        AnimationState(Animation animation, bool looping)
        {
            this.animation = animation;
            this.accumulator = TimeSpan.Zero;
            this.looping = looping;
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
        /// <returns>The bounding box of the current frame,
        /// or null if the animation has finished</returns>
        public Rectangle? update(GameTime dt)
        {
            accumulator += dt.ElapsedGameTime;
            frameNumber = (int) (accumulator.Ticks / animation.frameDuration.Ticks % animation.frameCount);
            if (!looping && accumulator.Ticks > animation.frameDuration.Ticks * animation.frameCount)
                return null;       
            return animation.bounds[frameNumber];
        }

        public void draw(SpriteBatch batch, Rectangle destination)
        {
            animation.draw(batch, destination, frameNumber);
        }
    }
}
