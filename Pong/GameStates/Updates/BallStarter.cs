using SDSMTGDT.GWorks.GameStates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Pong.GameStates.Models;

namespace Pong.GameStates.Updates
{
    class BallStarter : UpdateListener
    {
        public const int START_VELOCITY = 50;

        //ball which will begin moving
        private Ball ball;

        //viewport
        private Rectangle screen;

        //playstate to alter
        private MutableGameState playState;

        //reference to the object storing the current scores
        private Scoreboard scoreboard;

        public BallStarter (Ball ball, Rectangle screen, MutableGameState playState,
            Scoreboard scoreboard)
        {
            this.ball = ball;
            this.screen = screen;
            this.playState = playState;
            this.scoreboard = scoreboard;
        }

        public void Update(GameTime gameTime)
        {
            ball.setPosition(playState.StateManager.GraphicsDevice.Viewport.Width / 2,
                playState.StateManager.GraphicsDevice.Viewport.Height / 2);

            if ( chooseDirection() == true)
            {
                ball.setVelocity(START_VELOCITY, 0);
            }
            else
            {
                ball.setVelocity(-START_VELOCITY, 0);
            }
        }

        private bool chooseDirection()
        {
            if (scoreboard.scoreLeft > scoreboard.scoreRight)
            {
                return true;
            }
            return false;
        }
    }
}
