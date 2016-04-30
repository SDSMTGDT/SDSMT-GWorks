using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pong.BaseClasses;
using Microsoft.Xna.Framework;
using SDSMTGDT.GWorks.Physics;
using SDSMTGDT.GWorks.Events;
using Microsoft.Xna.Framework.Graphics;
using Pong.GameStates.Drawing;
using Pong.GameStates.Events;
using Pong.GameStates.Updates;

namespace Pong.GameStates.Models
{
    class Ball : MovingCollidableGameObject
    {
        public const int BALL_SIZE = 10;

        GraphicsDevice graphics;
        BallArtist artist;
        BallMovement movement;

        Ball(PhysicsManager physics, GraphicsDevice graphics)
            : base(new Rectangle(0,0,BALL_SIZE,BALL_SIZE), physics,
                  createBallTexture(graphics) , new Vector2(0,0), new Vector2(0,0))
        {
            artist = new BallArtist(this);
            movement = new BallMovement(this);
        }

        private static Texture2D createBallTexture( GraphicsDevice graphics)
        {
            return new Texture2D(graphics, BALL_SIZE, BALL_SIZE);
        }

    }
}
