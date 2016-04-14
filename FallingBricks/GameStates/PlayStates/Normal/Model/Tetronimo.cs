using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FallingBricks.GameStates.PlayStates.Normal.Model
{
    internal class Tetronimo
    {
        private List<BrickBrick> bricks;

        internal Tetronimo()
        {
            this.bricks = new List<BrickBrick>();
        }

        internal void addNewBrickBrick(int x, int y, Color color)
        {
            bricks.Add(new BrickBrick(x, y, color));
        }
    }
}
