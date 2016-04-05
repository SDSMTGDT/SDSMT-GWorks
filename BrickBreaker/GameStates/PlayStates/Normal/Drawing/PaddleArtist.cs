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
    class PaddleArtist : DrawListener
    {
        private Paddle paddle;

        internal PaddleArtist(Paddle paddle)
        {
            this.paddle = paddle;
        }

        public void draw(GameTime gameTime, SpriteBatch graphics)
        {
            graphics.Draw(paddle.getTexture(), paddle.getBounds(), Color.White);
        }

        public int getZIndex()
        {
            return 0;
        }
    }
}
