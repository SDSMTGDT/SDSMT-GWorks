using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pong.BaseClasses
{
    /// <summary>
    /// Game object is an interface that declares methods for obtaining an
    /// objects texture, velocity, and position.
    /// </summary>
    interface GameObject
    {
        Texture2D getTexture();
        Vector2 getPosition();
    }
}
