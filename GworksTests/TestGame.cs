using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SDSMTGDT.GWorks.GameStates;
using SDSMTGDT.GWorks.GameStates.ColorChanger;
using SDSMTGDT.GWorks.GameStates.Diagnostic;
using SDSMTGDT.GWorks.GameStates.MovingObjects;
using System;

namespace SDSMTGDT.DungeonCrawler
{
    /// <summary>
    /// A game class that will host all of our tests.
    /// </summary>
    public class TestGame : Game
    {
        GraphicsDeviceManager graphics;
        GameStateManager gStateManager;
        SpriteBatch spriteBatch;
        SpriteFont diagnosticFont;

        /// <summary>
        /// constructor for our test game.
        /// </summary>
        public TestGame()
        {
            graphics = new GraphicsDeviceManager(this);
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
            base.Initialize();
            // TODO: Add your initialization logic here
            gStateManager = new GameStateManager();

            Rectangle first = graphics.GraphicsDevice.Viewport.Bounds;
            first.Width = first.Width / 2;

            Rectangle second = first;
            second.X = second.Width;

            MutableGameState testState = new MutableGameState(gStateManager);

            MovingObject movingObject = new MovingObject(graphics.GraphicsDevice,
                new Rectangle(50, 0, 50, 50));

            testState.addUpdateListener(movingObject);
            testState.addDrawListener(movingObject);

            //Create new Screen color changers and add them to the game state
            //The Graphics Device creates the texture,
            //The second parameter sets the area to draw in
            testState.addDrawListener(new ScreenColorChanger(
                graphics.GraphicsDevice,
                first
            ));
            testState.addDrawListener(new ScreenColorChanger(
                graphics.GraphicsDevice,
                second
            ));

            //Create a new diagnostic display at (20, 20)
            testState.addDrawListener(new DiagnosticDisplay(20, 20, diagnosticFont));
            gStateManager.push(testState);
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            diagnosticFont = Content.Load<SpriteFont>("diagnostic");
            
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
            gStateManager.update(gameTime);
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            graphics.GraphicsDevice.Clear(Color.White);
            gStateManager.draw(gameTime, spriteBatch);
            base.Draw(gameTime);
        }
    }
}
