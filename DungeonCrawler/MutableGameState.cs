﻿using System;
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

        public override void draw(GameTime gameTime, GraphicsDeviceManager graphics)
        {
            foreach (DrawListener dl in drawListeners)
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

        public void addDrawListener(DrawListener dl)
        {
            LinkedListNode<DrawListener> dlNode = drawListeners.First;
            while(dlNode != null && dlNode.Value.getZIndex() > dl.getZIndex())
            {
                dlNode = dlNode.Next;
            }
            if (dlNode != null)
            {
                drawListeners.AddBefore(dlNode, dl);
            }
            else
            {
                drawListeners.AddFirst(dl);
            }
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
