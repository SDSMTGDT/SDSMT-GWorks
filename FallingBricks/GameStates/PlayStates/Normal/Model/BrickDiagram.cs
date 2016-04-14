using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FallingBricks.GameStates.PlayStates.Normal.Model
{
    struct BrickDiagram
    {
        internal int xSlot { get; private set; }
        internal int ySlot { get; private set; }
        internal int size { get; private set; }

        internal BrickDiagram(int xSlot, int ySlot, int size)
        {
            this.xSlot = xSlot;
            this.ySlot = ySlot;
            this.size = size;
        }
    }
}
