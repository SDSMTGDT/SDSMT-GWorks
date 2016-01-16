using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace SDSMTGDT.GWorks.GameStates
{
    public abstract class GameState
    {
        protected GameStateManager manager;

        public GameState(GameStateManager manager)
        {
            this.manager = manager;
        }
 
        public abstract void draw (
            GameTime gameTime,
            GraphicsDeviceManager graphics
        );

        public abstract void update(GameTime gameTime);

        public virtual void onAddState()
        {

        }

        public virtual void onRemoveState()
        {

        }
    }
}
