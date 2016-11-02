using BrickBreaker.GameStates.PlayStates.Normal.Model;
using SDSMTGDT.GWorks.GameStates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace BrickBreaker.GameStates.PlayStates.Normal.Updates
{
    /// <summary>
    /// Cruddy testing class for moving the ball where you want with wasd
    /// </summary>
    internal class ManualBallMovement : UpdateListener
    {
        private Ball ball;
        internal float MAX_SPEED { get; private set; } = .5F;

        internal ManualBallMovement(Ball ball)
        {
            this.ball = ball;
        }

        public void Update(GameTime gameTime)
        {
            Vector2 dir = new Vector2(0, 0);
            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                dir -= Vector2.UnitY;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                dir += Vector2.UnitY;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                dir -= Vector2.UnitX;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                dir += Vector2.UnitX;
            }
            if (dir != Vector2.Zero)
                ball.speedPxPerMillis = dir * MAX_SPEED;
        }
    }
}
