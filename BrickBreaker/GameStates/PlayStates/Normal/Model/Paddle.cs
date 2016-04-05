using BrickBreaker.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SDSMTGDT.GWorks.Physics;
using BrickBreaker.GameStates.PlayStates.Normal.Drawing;
using BrickBreaker.GameStates.PlayStates.Normal.Updates;

namespace BrickBreaker.GameStates.PlayStates.Normal.Model
{
    class Paddle : CollidableGameObject
    {
        private const short WIDTH = 100;
        private const short HEIGHT = 25;
        internal PaddleArtist artist { get; private set; }
        internal PaddleMovement movement { get; private set; }

        internal Paddle(GraphicsDevice device, NormalPlayState playState) : 
            base (setUpBounds(), setUpTexture(device), playState.stateManager.physics)
        {
            artist = new PaddleArtist(this);
            movement = new PaddleMovement(this, playState.screenWidth);
            setLocation(
                (playState.screenWidth - getBounds().Width) / 2,
                playState.screenHeight - getBounds().Height
            );
        }

        private static Rectangle setUpBounds()
        {
            return new Rectangle(0, 0, WIDTH, HEIGHT);
        }

        private static Texture2D setUpTexture(GraphicsDevice device)
        {
            Texture2D tex = new Texture2D(device, WIDTH, HEIGHT);
            tex.SetData(Enumerable.Repeat(Color.White, WIDTH * HEIGHT).ToArray());
            return tex;
        }
    }
}
