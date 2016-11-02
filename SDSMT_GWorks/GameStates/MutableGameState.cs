using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SDSMTGDT.GWorks.GameStates
{
    public class MutableGameState : GameState
    {
        private readonly LinkedList<DrawListener> drawListeners;
        private readonly LinkedList<UpdateListener> updateListeners;

        public MutableGameState(GameStateManager manager) : base(manager)
        {
            updateListeners = new LinkedList<UpdateListener>();
            drawListeners = new LinkedList<DrawListener>();
        }

        public override void Draw(GameTime gameTime, SpriteBatch graphics)
        {
            foreach (DrawListener dl in drawListeners)
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

        public void AddDrawListener(DrawListener dl)
        {
            LinkedListNode<DrawListener> dlNode = drawListeners.First;
            while(dlNode != null && dl.GetZIndex() < dlNode.Value.GetZIndex())
            {
                dlNode = dlNode.Next;
            }
            if (dlNode != null)
            {
                drawListeners.AddBefore(dlNode, dl);
            }
            else
            {
                drawListeners.AddLast(dl);
            }
        }

        public bool RemoveDrawListener(DrawListener dl)
        {
            return drawListeners.Remove(dl);
        }

        public void AddUpdateListener(UpdateListener ul)
        {
            updateListeners.AddLast(ul);
        }

        public bool RemoveUpdateListener(UpdateListener ul)
        {
            return updateListeners.Remove(ul);
        }
    }
}
