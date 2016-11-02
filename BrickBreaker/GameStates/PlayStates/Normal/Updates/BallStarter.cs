using SDSMTGDT.GWorks.GameStates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using BrickBreaker.GameStates.PlayStates.Normal.Model;
using Microsoft.Xna.Framework.Input;
using BrickBreaker.GameStates.PlayStates.Normal.Drawing;
using SDSMTGDT.GWorks.Events;

namespace BrickBreaker.GameStates.PlayStates.Normal.Updates
{
    internal class BallStarter : UpdateListener
    {
        //ball which will begin moving
        private Ball ball;

        //viewport
        private Rectangle screen;

        //playstate to alter
        private MutableGameState playState;

        /// <summary>
        /// Constructor of Ball Starters
        /// </summary>
        /// <param name="ball">Ball to start moving</param>
        /// <param name="screen">Bounds of the viewport </param>
        /// <param name="playState">The playstate to alter when starting the ball</param>
        internal BallStarter(Ball ball, Rectangle screen, MutableGameState playState)
        {
            this.ball = ball;
            this.screen = screen;
            this.playState = playState;
        }

        public void Update(GameTime gameTime)
        {
            //Create new ball and move it when the player presses space
            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                var rand = new Random();
                ball.setLocation(
                    (screen.Width - ball.GetBounds().Width) / 2,
                    (screen.Height - ball.GetBounds().Height) / 2
                );

                Vector2 ballVel = new Vector2(rand.Next(-50, 50), rand.Next(50 , 100));
                ballVel.Normalize();
                ballVel = ballVel * ball.movement.MAX_SPEED;

                ball.speedPxPerMillis = ballVel;

                //We cannot alter the playstate while were are updating
                //The alteration must happen after iteration over the update listeners
                //So, we use the event manager to queue an action to happen after the updates
                //have been run.
                playState.StateManager.Events.QueueAction(() => {
                    playState.AddDrawListener(ball.artist);
                    playState.AddUpdateListener(ball.movement);
                    //playState.addUpdateListener(new ManualBallMovement(ball));
                    //Don't allow this listener to run while the ball is in play
                    playState.RemoveUpdateListener(this);
                });
            }
        }

        /// <summary>
        /// Adds the ball starter back to the game state
        /// </summary>
        internal void resetBallStarter()
        {
            ball.setLocation(
                (screen.Width - ball.GetBounds().Width) / 2,
                (screen.Height - ball.GetBounds().Height) / 2
            );
            ball.speedPxPerMillis = new Vector2(0, 0);

            playState.StateManager.Events.QueueAction(() => {
                playState.RemoveDrawListener(ball.artist);
                playState.RemoveUpdateListener(ball.movement);
                playState.AddUpdateListener(this);
            });
        }
    }
}
