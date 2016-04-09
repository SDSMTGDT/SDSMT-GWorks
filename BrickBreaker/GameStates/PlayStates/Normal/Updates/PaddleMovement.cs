using SDSMTGDT.GWorks.GameStates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using BrickBreaker.GameStates.PlayStates.Normal.Model;
using Microsoft.Xna.Framework.Input;
using SDSMTGDT.GWorks.Settings;

namespace BrickBreaker.GameStates.PlayStates.Normal.Updates
{
    /// <summary>
    /// Moves the paddle back and forth
    /// </summary>
    internal class PaddleMovement : UpdateListener
    {
        private int screenWidth;
        private Paddle paddle;

        /// <summary>
        /// Creates a new update listener for a given paddle
        /// </summary>
        /// <param name="paddle">Paddle we're going to move</param>
        /// <param name="screenWidth">Viewport width</param>
        internal PaddleMovement(Paddle paddle, int screenWidth)
        {
            this.paddle = paddle;
            this.screenWidth = screenWidth;
        }

        public void update(GameTime gameTime)
        {
            var loc = paddle.getLocation();
            float xLoc = loc.X;
            if (Keyboard.GetState().IsKeyDown(Keys.Right) &&
                xLoc + paddle.getBounds().Width < screenWidth)
            {
                paddle.move((float)(paddle.speedPxPerMilli * gameTime.ElapsedGameTime.Milliseconds), 0);
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Left) && xLoc > 0)
            {
                paddle.move(-(float)(paddle.speedPxPerMilli * gameTime.ElapsedGameTime.Milliseconds), 0);
            }
        }
    }
}
