using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using SDSMTGDT.GWorks.Graphics;
using SDSMTGDT.GWorks.Settings;
using SDSMTGDT.GWorks.Events;
using Microsoft.Xna.Framework.Graphics;
using SDSMTGDT.GWorks.Physics;

namespace SDSMTGDT.GWorks.GameStates
{
    public class GameStateManager
    {
        private readonly Stack<GameState> states;
        public SettingsManager Settings { get; }
        public EventManager Events { get; }
        public CollisionManager Collisions { get; private set; }
        public GraphicsDevice GraphicsDevice { get; private set; }

        public GameStateManager(GraphicsDevice graphics)
        {
            states = new Stack<GameState>();
            Settings = new Settings.SettingsManager();
            Events = new EventManager();
            Collisions = new CollisionManager(Events);
            GraphicsDevice = graphics;

            //sometimes graphics is null when we don't need drawing
            if (graphics != null) 
            {
                Settings.Update(
                    Settings.EngineSettings.WINDOW_WIDTH,
                    graphics.Viewport.Width
                );
                Settings.Update(
                    Settings.EngineSettings.WINDOW_HEIGHT,
                    graphics.Viewport.Height
                );
            }
        }

        public void Push (GameState state)
        {
            state.OnAddState();
            states.Push(state);
        }

        public GameState Pop ()
        {
            states.Peek().OnRemoveState();
            return states.Pop();
        }

        public GameState Top ()
        {
            return states.Peek();
        }

        public void Update(GameTime gameTime)
        {
            if (states.Count != 0)
                states.Peek().Update(gameTime);
            Events.Update(gameTime);
        }

        public void Draw(GameTime gameTime, SpriteBatch graphics)
        {
            if (states.Count == 0)
                return;
            graphics.Begin();
            states.Peek().Draw(gameTime, graphics);
            graphics.End();
        }
    }
}
