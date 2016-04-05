using SDSMTGDT.GWorks.GameStates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using BrickBreaker.GameStates.PlayStates.Normal.Model;

namespace BrickBreaker.GameStates.PlayStates.Normal.Drawing
{
    class BallArtist : DrawListener
    {
        private Ball ball;

        internal BallArtist(Ball ball)
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
