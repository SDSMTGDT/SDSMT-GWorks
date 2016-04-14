using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FallingBricks.Helpers
{
    interface GraphicsObject
    {
        Texture2D getTexture();

        Rectangle getDrawBounds();
    }
}
