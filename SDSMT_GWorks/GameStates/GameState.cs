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
        public GameStateManager StateManager { get; }

        //TODO: implement this
        private Queue<Action> AfterRunQueue { get; }

        public GameState(GameStateManager manager)
        {
            StateManager = manager;
            AfterRunQueue = new Queue<Action>();
        }
 
        public abstract void Draw (
            GameTime gameTime,
            SpriteBatch graphics
        );

        public abstract void Update(GameTime gameTime);

        public virtual void OnAddState()
        {

        }

        public virtual void OnRemoveState()
        {

        }

        public virtual void OnLoadState(ContentManager content)
        {

        }

        public virtual void OnUnloadState()
        {
            
        }
    }
}
