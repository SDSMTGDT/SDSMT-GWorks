using SDSMTGDT.GWorks.GameStates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pong.GameStates.Models;

namespace Pong.GameStates.Drawing
{
    class BallArtist : DrawListener
    {
        Ball ball;

        public BallArtist (Ball ball)
        {
            this.ball = ball;
        }

        public void draw(GameTime gameTime, SpriteBatch graphics)
        {
            graphics.Draw(ball.getTexture(), ball.getBounds(), Color.White);
        }

        public int getZIndex()
        {
            return 0;
        }
    }
}
