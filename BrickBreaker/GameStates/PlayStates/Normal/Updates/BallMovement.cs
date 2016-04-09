using BrickBreaker.GameStates.PlayStates.Normal.Model;
using SDSMTGDT.GWorks.GameStates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace BrickBreaker.GameStates.PlayStates.Normal.Updates
{
    /// <summary>
    /// Update listener used to move the ball along
    /// </summary>
    internal class BallMovement : UpdateListener
    {
        //reference to the ball its moving
        private Ball ball;

        //the max speed at which the ball should move
        internal float MAX_SPEED { get; private set; } = .5F;

        internal BallMovement(Ball ball)
        {
            this.ball = ball;
        }

        public void update(GameTime gameTime)
        {
            var movement = ball.speedPxPerMillis * gameTime.ElapsedGameTime.Milliseconds;
            ball.move(movement.X, movement.Y);
        }
    }
}
