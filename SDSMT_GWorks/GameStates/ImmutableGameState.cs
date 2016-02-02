﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SDSMTGDT.GWorks.GameStates
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
            // Sorts by z level with lower z indexes last
            this.drawListeners = drawListeners.OrderByDescending(
                (drawListener) => drawListener.getZIndex()
            );
        }

        public override void draw(GameTime gameTime, SpriteBatch graphics)
        {
            foreach(DrawListener dl in drawListeners)
            {
                dl.draw(gameTime, graphics);
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