using SDSMTGDT.GWorks.GameStates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Pong.GameStates.Models;

namespace Pong.GameStates.Updates
{
    class BallMovement : UpdateListener
    {
        //the ball being updated
        Ball ball;

        /// <summary>
        /// creates a new BallMovement update listener
        /// </summary>
        /// <param name="ball"> the ball being updated</param>
        public BallMovement(Ball ball)
        {
            this.ball = ball;
        }

        /// <summary>
        /// adjust the ball's position based on its velocity.
        /// </summary>
        /// <param name="gameTime"> time since last update</param>
        public void update(GameTime gameTime)
        {
            ball.move(gameTime.ElapsedGameTime.Milliseconds);
        }
    }
}
