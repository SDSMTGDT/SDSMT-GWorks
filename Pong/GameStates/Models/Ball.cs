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
    class Ball : CollidableGameObject
    {
        BallArtist artist;
        BallMovement movement;

        Ball (Rectangle bounds, PhysicsManager physics, Texture2D texture,
            Vector2 startingVelocity, Vector2 position, GraphicsDevice graphics)
            : base(bounds, physics, texture, startingVelocity,position,graphics)
        {
            artist = new BallArtist();
            movement = new BallMovement();
        }


    }
}
