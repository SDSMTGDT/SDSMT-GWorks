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
        private Stack<GameState> states;
        public SettingsManager settings { get; private set; }
        public EventManager events { get; private set; }
        public PhysicsManager physics { get; private set; }
        public GraphicsDevice graphicsDevice { get; private set; }

        public GameStateManager(GraphicsDevice graphics)
        {
            states = new Stack<GameState>();
            settings = new Settings.SettingsManager();
            events = new EventManager();
            physics = new PhysicsManager(events);
            this.graphicsDevice = graphics;

            settings.update(
                settings.engineSettings.WINDOW_WIDTH,
                graphics.Viewport.Width
            );
            settings.update(
                settings.engineSettings.WINDOW_HEIGHT,
                graphics.Viewport.Height
            );
        }

        public void push (GameState state)
        {
            state.onAddState();
            states.Push(state);
        }

        public GameState pop ()
        {
            states.Peek().onRemoveState();
            return states.Pop();
        }

        public GameState top ()
        {
            return states.Peek();
        }

        public void update(GameTime gameTime)
        {
            if (states.Count != 0)
                states.Peek().update(gameTime);
            events.update(gameTime);
        }

        public void draw(GameTime gameTime, SpriteBatch graphics)
        {
            if (states.Count == 0)
                return;
            graphics.Begin();
            states.Peek().draw(gameTime, graphics);
            graphics.End();
        }
    }
}
