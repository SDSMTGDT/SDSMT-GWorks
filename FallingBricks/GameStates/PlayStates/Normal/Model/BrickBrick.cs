using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FallingBricks.GameStates.PlayStates.Normal.Model
{
    internal struct BrickBrick
    {
        internal int x { get; private set; }
        internal int y { get; private set; }

        internal Color color { get; private set; }

        internal BrickBrick(int x, int y, Color color)
        {
            this.x = x;
            this.y = y;
            this.color = color;
        }
    }
}
