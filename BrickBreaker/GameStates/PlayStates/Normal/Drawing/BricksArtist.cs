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
    class BricksArtist : DrawListener
    {
        private Brick[,] bricks;

        internal BricksArtist(Brick[,] bricks)
        {
            this.bricks = bricks;
        }
        
        public void Draw(GameTime gameTime, SpriteBatch graphics)
        {

            foreach (var brick in bricks)
            { 
                if (!brick.destroyed)
                    graphics.Draw(brick.texture, brick.GetBounds(), brick.color);
            }
        }

        public int GetZIndex()
        {
            return 0;
        }
    }
}
