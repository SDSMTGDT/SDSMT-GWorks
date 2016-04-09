using BrickBreaker.GameStates.PlayStates.Normal;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SDSMTGDT.GWorks.Events;
using SDSMTGDT.GWorks.GameStates;
using SDSMTGDT.GWorks.Physics;
using SDSMTGDT.GWorks.Settings;

namespace BrickBreaker
{
    /// <summary>
    /// This is the main type for a MonoGame based Game
    /// </summary>
    public class BrickBreaker : Game //Inherit from Game in order to bring in MonoGame
    {
        /// <summary>
        /// Handles drawing to the screen via sprite batches and other objects
        /// </summary>
        GraphicsDeviceManager graphics;

        /// <summary>
        /// Essentially a group of draw commands to be executed at once
        /// </summary>
        SpriteBatch spriteBatch;

        /// <summary>
        /// SDSMT GWorks Game State Manager. Controls game states, gworks settings, etc.
        /// </summary>
        GameStateManager gameStateManager;

        /// <summary>
        /// Constructor for the main game
        /// </summary>
        public BrickBreaker()
        {
            //Create a new graphics manager from the information passed into the game by xna
            graphics = new GraphicsDeviceManager(this);

            //Sets up the content directory for images sound etc
            Content.RootDirectory = "Content";
        }


        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            //Let monogame initialize the game super class
            base.Initialize();

            //Initialize a new Game State Manager for Gworks with a graphics device
            gameStateManager = new GameStateManager(graphics.GraphicsDevice);
            
            
            gameStateManager.push(new NormalPlayState(gameStateManager));
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            gameStateManager.update(gameTime);
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            gameStateManager.draw(gameTime, spriteBatch);
            base.Draw(gameTime);
        }
    }
}
