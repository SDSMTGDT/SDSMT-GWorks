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
        //ball which will begin moving
        private Ball ball;

        //viewport
        private Rectangle screen;

        //playstate to alter
        private MutableGameState playState;

        private Scoreboard scoreboard;

        public BallStarter (Ball ball, Rectangle screen, MutableGameState playState,
            Scoreboard scoreboard)
        {
            this.ball = ball;
            this.screen = screen;
            this.playState = playState;
            this.scoreboard = scoreboard;
        }

        public void update(GameTime gameTime)
        {
            ball.setPosition(playState.stateManager.graphicsDevice.Viewport.Width / 2,
                playState.stateManager.graphicsDevice.Viewport.Height / 2);

            if ( chooseDirection() == true)
            {
                
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
