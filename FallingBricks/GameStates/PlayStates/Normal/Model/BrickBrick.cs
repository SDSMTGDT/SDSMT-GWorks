using FallingBricks.Helpers;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace FallingBricks.GameStates.PlayStates.Normal.Model
{
    internal class BrickBrick : GameObject
    {

        internal BrickBrick(int x, int y, Vector2 velocity, int size, Color color, GraphicsDevice graphics)
        {
            Texture2D texture = new Texture2D(graphics, size, size);
            texture.SetData(Enumerable.Repeat(color, size * size).ToArray());
            setTexture(texture);
            setBounds(x, y, size, size);
            setVelocity(velocity);
        }
    }
}
