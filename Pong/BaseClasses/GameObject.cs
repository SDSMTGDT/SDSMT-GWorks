using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pong.BaseClasses
{
    /// <summary>
    /// Game object is the super class for all objects. Defines basic movement,
    /// object position, and texture.
    /// </summary>
    class GameObject
    {
        private Texture2D texture; /*the texture for the object*/
        private Vector2 velocity; /*the x and y velocities of the object*/
        private GraphicsDevice graphics; /* */
        
        protected GameObject ()
        {
            texture = new Texture2D()
        }
    }
}
