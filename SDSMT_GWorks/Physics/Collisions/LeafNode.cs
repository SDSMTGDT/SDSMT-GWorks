using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace SDSMTGDT.GWorks.Physics.Collisions
{
    public class QuadLeafNode : QuadTreeNode
    {
        private Rectangle bounds;

        private List<Collidable> leafContents;
        private QuadStemNode parent;

        public QuadLeafNode(QuadStemNode parent, Rectangle bounds)
        {
            leafContents = new List<Collidable>();
            this.parent = parent;
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

        private void Split(QuadStemNode newChild)
        {
            newChild = new QuadStemNode(this.bounds);
            foreach(Collidable c in leafContents)
            {
                newChild.insert(c);
            }
            parent.replaceNode(this, newChild);
        }
    }
}
