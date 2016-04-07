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
    class BallStarter : UpdateListener
    {
        private Ball ball;
        private NormalPlayState playState;

        internal BallStarter(Ball ball, NormalPlayState playState)
        {
            this.ball = ball;
            this.playState = playState;
        }

        public void update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                var rand = new Random();
                ball.setLocation(
                    (playState.screenWidth - ball.getBounds().Width) / 2,
                    (playState.screenHeight - ball.getBounds().Height) / 2
                );

                Vector2 ballVel = new Vector2(rand.Next(-50, 50), rand.Next(50 , 100));
                ballVel.Normalize();
                ballVel = ballVel * ball.movement.MAX_SPEED;

                ball.speedPxPerMillis = ballVel;

                playState.stateManager.events.queueAction(() => {
                    playState.addDrawListener(ball.artist);
                    playState.addUpdateListener(ball.movement);
                    //playState.addUpdateListener(new ManualBallMovement(ball));
                    playState.removeUpdateListener(this);
                });
            }
        }

        internal void resetBallStarter()
        {
            ball.setLocation(
                (playState.screenWidth - ball.getBounds().Width) / 2,
                (playState.screenHeight - ball.getBounds().Height) / 2
            );
            ball.speedPxPerMillis = new Vector2(0, 0);

            playState.stateManager.events.queueAction(() => {
                playState.removeDrawListener(ball.artist);
                playState.removeUpdateListener(ball.movement);
                playState.addUpdateListener(this);
            });
        }
    }
}
