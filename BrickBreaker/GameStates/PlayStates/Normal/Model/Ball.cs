using BrickBreaker.GameStates.PlayStates.Normal.Drawing;
using BrickBreaker.GameStates.PlayStates.Normal.Events;
using BrickBreaker.GameStates.PlayStates.Normal.Updates;
using BrickBreaker.Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SDSMTGDT.GWorks.GameStates;
using SDSMTGDT.GWorks.Physics;
using SDSMTGDT.GWorks.Physics.Collisions.Routes;
using SDSMTGDT.GWorks.Spriting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrickBreaker.GameStates.PlayStates.Normal.Model
{
    class Ball : CollidableGameObject
    {
        private const short WIDTH = 20;
        private const short HEIGHT = 20;

        internal Vector2 speedPxPerMillis { get; set; }
        internal BallArtist artist { get; private set; }
        internal BallMovement movement { get; private set; }
        internal BallStarter starter { get; private set; }

        internal Ball(MutableGameState gameState, Rectangle screen, GraphicsDevice graphicsDevice, CollisionManager collisions) : 
            base (setUpBounds(), setUpTexture(graphicsDevice), collisions)
        {
            speedPxPerMillis = new Vector2(0, 0);

            CollisionEventRouter router = new CollisionEventRouter();
            router.addCollisionRoute(new TypeCollisionRoute<Ball, EastWall>(Collisions.handleBallEastWallCollision));
            router.addCollisionRoute(new TypeCollisionRoute<Ball, WestWall>(Collisions.handleBallWestWallCollision));
            router.addCollisionRoute(new TypeCollisionRoute<Ball, NorthWall>(Collisions.handleBallNorthWallCollision));
            router.addCollisionRoute(new TypeCollisionRoute<Ball, SouthWall>(Collisions.handleBallSouthWallCollision));
            router.addCollisionRoute(new TypeCollisionRoute<Ball, Paddle>(Collisions.handleBallPaddleCollision));
            router.addCollisionRoute(new TypeCollisionRoute<Ball, Brick>(Collisions.handleBallBrickCollision));

            collisionPublisher.registerEventSubscriber(router);

            artist = new BallArtist(this);
            movement = new BallMovement(this);
            starter = new BallStarter(this, screen, gameState);
            setLocation(-100, -100);
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
