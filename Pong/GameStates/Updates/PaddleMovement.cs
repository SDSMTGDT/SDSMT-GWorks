using SDSMTGDT.GWorks.GameStates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Pong.GameStates.Models;
using Microsoft.Xna.Framework.Input;

namespace Pong.GameStates.Updates
{
    class PaddleMovement : UpdateListener
    {
        public const int PADDLE_MOVE_SPEED = 50;

        //the paddle being updated
        Paddle paddle;

        public PaddleMovement(Paddle paddle)
        {
            this.paddle = paddle;
        }

        public void update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                paddle.setVelocity(0, PADDLE_MOVE_SPEED);
            }
            else if ( Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                paddle.setVelocity(0, -PADDLE_MOVE_SPEED);
            }
            else
            {
                paddle.setVelocity(0,0);
            }
            paddle.move(gameTime.ElapsedGameTime.Milliseconds);
        }
    }
}
