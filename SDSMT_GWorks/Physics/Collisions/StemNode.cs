using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace SDSMTGDT.GWorks.Physics.Collisions
{
    public class QuadStemNode : QuadTreeNode
    {
        private Rectangle bounds;

        private QuadTreeNode Q1;
        private QuadTreeNode Q2;
        private QuadTreeNode Q3;
        private QuadTreeNode Q4;

        public QuadStemNode(Rectangle bounds)
        {
            Q1 = new QuadLeafNode(this, new Rectangle(bounds.X + bounds.Width / 2, bounds.Y, bounds.Width / 2, bounds.Height / 2));
            Q2 = new QuadLeafNode(this, new Rectangle(bounds.X, bounds.Y, bounds.Width / 2, bounds.Height / 2));
            Q3 = new QuadLeafNode(this, new Rectangle(bounds.X, bounds.Y + bounds.Height/2, bounds.Width / 2, bounds.Height / 2));
            Q4 = new QuadLeafNode(this, new Rectangle(bounds.X + bounds.Width / 2, bounds.Y + bounds.Height/2, bounds.Width / 2, bounds.Height / 2));
            this.bounds = bounds;
        }

        public Rectangle getBounds()
        {
            return bounds;
        }

        public void delete(Collidable c)
        {
            throw new NotImplementedException();
        }

        public void insert(Collidable c)
        {
            throw new NotImplementedException();
        }

        public List<Collidable> searchNode(Rectangle r)
        {
            throw new NotImplementedException();
        }

        internal void replaceNode(QuadTreeNode old, QuadTreeNode newNode)
        {
            if ( old == Q1 )
            {
                Q1 = newNode;
            }
            else if ( old == Q2 )
            {
                Q2 = newNode;
            }
            else if ( old == Q3 )
            {
                Q3 = newNode;
            }
            else if ( old == Q4 )
            {
                Q4 = newNode;
            }
        }
    }
}
