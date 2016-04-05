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
    class BallMovement : UpdateListener
    {
        private Ball ball;
        internal float MAX_SPEED { get; private set; } = .4F;

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
