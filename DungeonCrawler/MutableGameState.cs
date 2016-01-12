using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace SDSMTGDT.DungeonCrawler
{
    public class MutableGameState : GameState
    {
        private LinkedList<DrawListener> drawListeners;
        private LinkedList<UpdateListener> updateListeners;

        public MutableGameState(GameStateManager manager) : base(manager)
        {
            this.updateListeners = new LinkedList<UpdateListener>();
            this.drawListeners = new LinkedList<DrawListener>();
        }

        public override void draw(GameTime gameTime)
        {
            foreach (DrawListener dl in drawListeners)
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

        public void addDrawListener(DrawListener dl)
        {
            drawListeners.AddLast(dl);
        }

        public bool removeDrawListener(DrawListener dl)
        {
            return drawListeners.Remove(dl);
        }

        public void addUpdateListener(UpdateListener ul)
        {
            updateListeners.AddLast(ul);
        }

        public bool removeUpdateListener(UpdateListener ul)
        {
            return updateListeners.Remove(ul);
        }
    }
}
