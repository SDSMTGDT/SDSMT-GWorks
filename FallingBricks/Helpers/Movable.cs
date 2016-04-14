using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FallingBricks.Helpers
{
    interface Movable
    {
        void setVelocity(Vector2 velocity);

        void setVelocity(float x, float y);

        void move(GameTime gameTime);
    }
}
