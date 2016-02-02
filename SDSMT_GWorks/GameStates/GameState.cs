using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

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
            SpriteBatch graphics
        );

        public abstract void update(GameTime gameTime);

        public virtual void onAddState()
        {

        }

        public virtual void onRemoveState()
        {

        }

        public virtual void onLoadState()
        {

        }

        public virtual void onUnloadState()
        {

        }
    }
}
