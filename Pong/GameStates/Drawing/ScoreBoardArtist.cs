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
    class ScoreBoardArtist : DrawListener
    {
        Scoreboard scoreboard;

        public void Draw(GameTime gameTime, SpriteBatch graphics)
        {
            graphics.Draw(scoreboard.texture, scoreboard.bounds, Color.White);
        }

        public int GetZIndex()
        {
            return 0;
        }
    }
}
