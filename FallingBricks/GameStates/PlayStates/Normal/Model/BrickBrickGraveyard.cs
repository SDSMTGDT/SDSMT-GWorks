using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FallingBricks.GameStates.PlayStates.Normal.Model
{
    class BrickBrickGraveyard
    {
        internal List<BrickBrick> bricks { get; private set; }

        internal BrickBrickGraveyard()
        {
            bricks = new List<BrickBrick>();
        }
    }
}
