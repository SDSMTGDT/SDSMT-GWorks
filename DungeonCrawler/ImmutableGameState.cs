using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace SDSMTGDT.DungeonCrawler
{
    public class ImmutableGameState : GameState
    {
        private IEnumerable<DrawListener> drawListeners;
        private IEnumerable<UpdateListener> updateListeners;

        public ImmutableGameState(IEnumerable<DrawListener> drawListeners,
            IEnumerable<UpdateListener> updateListeners,
            GameStateManager manager) : base(manager)
        {
            this.updateListeners = updateListeners;
            this.drawListeners = drawListeners;
        }

        public override void draw(GameTime gameTime)
        {
            foreach(DrawListener dl in drawListeners)
            {
                dl.draw(gameTime);
            }
        }

        public override void update(GameTime gameTime)
        {
            foreach (UpdateListener ul in updateListeners)
            {
                ul.update(gameTime);
            }
        }
    }
}
