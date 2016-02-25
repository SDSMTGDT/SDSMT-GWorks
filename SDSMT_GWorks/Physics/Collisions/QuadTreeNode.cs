using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDSMTGDT.GWorks.Physics.Collisions
{
    interface QuadTreeNode
    {
        Rectangle getBounds();
        void insert(Collidable c);
        void delete(Collidable c);
        List<Collidable> searchNode(Rectangle r);
    }
}
