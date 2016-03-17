using SDSMTGDT.GWorks.Physics.Collisions.DataStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDSMTGDT.GWorks.Physics.Collisions
{
    public class CollisionGroup
    {
        public uint GROUP_ID { get; private set; }
        public string name { get; private set; }
  
        internal CollisionStructure structure { get; private set; }

        internal CollisionGroup(uint id, string name, CollisionStructure structure)
        {
            this.GROUP_ID = id;
            this.name = name;
            this.structure = structure;
        }
    }
}
