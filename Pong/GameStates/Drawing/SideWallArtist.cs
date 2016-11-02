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
    class SideWallArtist : DrawListener
    {
        SideWall sidewall;

        public void Draw(GameTime gameTime, SpriteBatch graphics)
        {
            graphics.Draw(sidewall.getTexture(), sidewall.GetBounds(), Color.Green);
        }

        public int GetZIndex()
        {
            return 0;
        }
    }
}
