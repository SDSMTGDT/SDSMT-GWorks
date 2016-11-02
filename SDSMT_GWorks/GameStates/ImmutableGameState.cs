using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SDSMTGDT.GWorks.GameStates
{
    public class ImmutableGameState : GameState
    {
        private readonly IEnumerable<DrawListener> drawListeners;
        private readonly IEnumerable<UpdateListener> updateListeners;

        public ImmutableGameState(IEnumerable<DrawListener> drawListeners,
            IEnumerable<UpdateListener> updateListeners,
            GameStateManager manager) : base(manager)
        {
            this.updateListeners = updateListeners;
            // Sorts by z level with lower z indexes last
            this.drawListeners = drawListeners.OrderByDescending(
                (drawListener) => drawListener.GetZIndex()
            );
        }

        public override void Draw(GameTime gameTime, SpriteBatch graphics)
        {
            foreach(DrawListener dl in drawListeners)
            {
                dl.Draw(gameTime, graphics);
            }
        }

        public override void Update(GameTime gameTime)
        {
            foreach (UpdateListener ul in updateListeners)
            {
                ul.Update(gameTime);
            }
        }
    }
}
