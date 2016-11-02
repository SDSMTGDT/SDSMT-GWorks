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
    /// <summary>
    /// The normal play state is the bread and butter of the brick breaker game
    /// </summary>
    //Inherit from mutable game state in order to change update listeners on the fly
    internal class NormalPlayState : MutableGameState 
    {
        //Paddle object contains update listener for movement, draw artist for graphics, and other info
        private Paddle paddle; 

        //Ball object contains update listener for movement, draw artist for graphics, and other info
        private Ball ball;

        //A 2d array of bricks
        private Brick[,] bricks;

        //I decided to implement all wall collisions using the collisions system
        //So we create some walls
        private Wall north, east, south, west;

        //Variables for creating the brick field
        private const int numBricksWide = 20;
        private const int numBricksHigh = 10;

        //Auto set via the above variables
        private int brickWidth;
        private int brickHeight;

        //Obtained from the settings manager
        internal int screenWidth { get; private set; }
        internal int screenHeight { get; private set; }

        //We put all collidable objects in the same collision group
        private CollisionGroup collisionGroup;

        /// <summary>
        /// Constructor for the normal play state. Sets up the brick field and collision group
        /// Does not create models etc until the state is added to the game state manager
        /// </summary>
        /// <param name="manager">Game state manager which will run this state</param>
        internal NormalPlayState(GameStateManager manager) : base(manager)
        {
            screenWidth = manager.Settings.Access(manager.Settings.EngineSettings.WINDOW_WIDTH);
            screenHeight = manager.Settings.Access(manager.Settings.EngineSettings.WINDOW_HEIGHT);
            brickWidth = screenWidth / numBricksWide;
            brickHeight = (screenHeight / 3) / numBricksHigh;
            collisionGroup = manager.Collisions.CreateCollisionGroup("BrickBreaker", new CollisionListFactory());
        }

        /// <summary>
        /// This method is called when the game state is pushed to the game state manager
        /// </summary>
        public override void OnAddState()
        {
            //Generate a screen rectangle for wall collidables
            Rectangle screen = new Rectangle(0, 0, screenWidth, screenHeight);
            
            //Create walls
            this.north = new NorthWall(screen, StateManager.Collisions);
            this.east = new EastWall(screen, StateManager.Collisions);
            this.south = new SouthWall(screen, StateManager.Collisions);
            this.west = new WestWall(screen, StateManager.Collisions);

            // pass this for access to game state manager for resouces
            this.paddle = new Paddle(screen, StateManager.GraphicsDevice, StateManager.Collisions);
            
            // pass this for access to game state manager for resources
            this.ball = new Ball(this, screen, StateManager.GraphicsDevice, StateManager.Collisions);

            // create the array of bricks
            this.bricks = new Brick[numBricksHigh, numBricksWide];

            // fill in bricks
            for (int i = 0; i < numBricksHigh; i++)
            {
                //Get the y coordinate of the new brick
                int y = brickHeight * i;

                for (int j = 0; j < numBricksWide; j++)
                {
                    //Get the x coordinate of the new brick
                    int x = brickWidth * j;

                    bricks[i, j] = new Brick(StateManager.GraphicsDevice, Color.Red,
                        new Rectangle(x, y, brickWidth, brickHeight), StateManager.Collisions);
                    /*if (i * j % 2 == 0)
                        bricks[i, j].destroyed = true;
                    else*/
                        StateManager.Collisions.RegisterCollidableInGroup(bricks[i, j], collisionGroup);                   
                }
            }

            AddDrawListener(paddle.artist);
            AddDrawListener(new BricksArtist(bricks));
            AddUpdateListener(paddle.movement);
            AddUpdateListener(ball.starter);

            StateManager.Collisions.RegisterCollidableInGroup(paddle, collisionGroup);
            StateManager.Collisions.RegisterCollidableInGroup(ball, collisionGroup);
            StateManager.Collisions.RegisterCollidableInGroup(north, collisionGroup);
            StateManager.Collisions.RegisterCollidableInGroup(east, collisionGroup);
            StateManager.Collisions.RegisterCollidableInGroup(south, collisionGroup);
            StateManager.Collisions.RegisterCollidableInGroup(west, collisionGroup);
            
            //Collision pumper just tells the library to check for collisions every tick... because im lazy
            AddUpdateListener(new CollisionChecker(StateManager.Collisions, ball));
        }
    }
}
