using Pong.BaseClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pong.GameStates.Models
{
    class Scoreboard : GameObject
    {
        public int scoreLeft { get; set; }
        public int scoreRight { get; set; }

        public Texture2D texture;
        public Vector2 position;
        public Rectangle bounds;

        public Scoreboard()
        {
            scoreLeft = 0;
            scoreRight = 0;
            bounds = new Rectangle(0, 0, 50, 50);
        }

        public Texture2D getTexture()
        {
            return texture;
        }

        public Vector2 getPosition()
        {
            return position;
        }
    }
}
