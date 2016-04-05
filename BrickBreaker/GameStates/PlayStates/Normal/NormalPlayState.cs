using BrickBreaker.GameStates.PlayStates.Normal.Drawing;
using BrickBreaker.GameStates.PlayStates.Normal.Model;
using BrickBreaker.GameStates.PlayStates.Normal.Updates;
using Microsoft.Xna.Framework;
using SDSMTGDT.GWorks.GameStates;
using SDSMTGDT.GWorks.Physics.Collisions;
using SDSMTGDT.GWorks.Physics.Collisions.DataStructures.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrickBreaker.GameStates.PlayStates.Normal
{
    class NormalPlayState : MutableGameState
    {
        private Paddle paddle;
        private Ball ball;
        private Brick[,] bricks;
        private Wall north, east, south, west;
        private const int numBricksWide = 20;
        private const int numBricksHigh = 5;
        private int brickWidth;
        private int brickHeight;
        internal int screenWidth { get; private set; }
        internal int screenHeight { get; private set; }

        private CollisionGroup collisionGroup;

        internal NormalPlayState(GameStateManager manager) : base(manager)
        {
            screenWidth = manager.settings.access(manager.settings.engineSettings.WINDOW_WIDTH);
            screenHeight = manager.settings.access(manager.settings.engineSettings.WINDOW_HEIGHT);
            brickWidth = screenWidth / numBricksWide;
            brickHeight = (int)(.5 * brickWidth);
            collisionGroup = manager.physics.createCollisionGroup("BrickBreaker", new CollisionListFactory());
        }

        public override void onAddState()
        {
            //Generate a screen rectangle
            Rectangle screen = new Rectangle(0, 0, screenWidth, screenHeight);
            this.north = new NorthWall(screen, stateManager.physics);
            this.east = new EastWall(screen, stateManager.physics);
            this.south = new SouthWall(screen, stateManager.physics);
            this.west = new WestWall(screen, stateManager.physics);

            // graphics device to create texture, physics for collisions, screenwidth for centering
            this.paddle = new Paddle(stateManager.graphicsDevice, this);
            
            this.ball = new Ball(stateManager.graphicsDevice, this);

            this.bricks = new Brick[numBricksHigh, numBricksWide];

            // fill in bricks
            for (int i = 0; i < numBricksHigh; i++)
            {
                int y = brickHeight * i;
                for (int j = 0; j < numBricksWide; j++)
                {
                    int x = brickWidth * j;
                    bricks[i, j] = new Brick(stateManager.graphicsDevice, Color.Red,
                        new Rectangle(x, y, brickWidth, brickHeight), stateManager.physics);
                }
            }

            addDrawListener(paddle.artist);
            addDrawListener(new BricksArtist(bricks));
            addUpdateListener(paddle.movement);
            addUpdateListener(ball.starter);

            stateManager.physics.registerCollidableInGroup(paddle, collisionGroup);
            stateManager.physics.registerCollidableInGroup(ball, collisionGroup);
            stateManager.physics.registerCollidableInGroup(north, collisionGroup);
            stateManager.physics.registerCollidableInGroup(east, collisionGroup);
            stateManager.physics.registerCollidableInGroup(south, collisionGroup);
            stateManager.physics.registerCollidableInGroup(west, collisionGroup);

            foreach (var brick in bricks)
            {
                stateManager.physics.registerCollidableInGroup(brick, collisionGroup);
            }
            addUpdateListener(new CollisionPumper(stateManager.physics));
        }
    }
}
