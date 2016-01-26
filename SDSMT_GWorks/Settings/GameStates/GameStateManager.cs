using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using SDSMTGDT.GWorks.Graphics;
using SDSMTGDT.GWorks.Settings;
using SDSMTGDT.Gworks.Events;

namespace SDSMTGDT.GWorks.GameStates
{
    public class GameStateManager
    {
        private Stack<GameState> states;
        private SettingsManager settings;
        private EventManager eventManager;
        private Camera2d overWorldCamera;
        private Camera2d dungeonCamera;
        private Camera2d standardCamera;

        public GameStateManager()
        {
            states = new Stack<GameState>();
            settings = new Settings.SettingsManager();
            eventManager = new EventManager();
            overWorldCamera = new Camera2d();
            dungeonCamera = new Camera2d();
            standardCamera = new Camera2d();
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
            eventManager.processQueuedEvents();
        }

        public void draw(GameTime gameTime, GraphicsDeviceManager graphics)
        {
            if (states.Count == 0)
                return;
            states.Peek().draw(gameTime, graphics);
        }
        public Settings.SettingsManager getSettingsManager()
        {
            return settings;
        }

        public EventManager getEventManager()
        {
            return eventManager;
        }
    }
}
