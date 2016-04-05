using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace SDSMTGDT.GWorks.GameStates
{
    public abstract class GameState
    {
        public GameStateManager stateManager;
        private Queue<Action> afterRunQueue;

        public GameState(GameStateManager manager)
        {
            this.stateManager = manager;
            this.afterRunQueue = new Queue<Action>();
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

        public virtual void onLoadState(ContentManager content)
        {

        }

        public virtual void onUnloadState()
        {
            
        }
    }
}
