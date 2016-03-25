using SDSMTGDT.GWorks.Physics.Collisions.DataStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDSMTGDT.GWorks.Physics.Collisions
{
    /// <summary>
    /// A collision group groups collidables into a specified CollisionStructure (which may be
    /// a tree, list, etc). Additionally, all collision groups must be named and have a unique id.
    /// </summary>
    public class CollisionGroup
    {
        /// <summary>
        /// The unique id for this group
        /// </summary>
        public uint GROUP_ID { get; private set; }

        /// <summary>
        /// The name of this group
        /// </summary>
        public string name { get; private set; }
  
        /// <summary>
        /// The backing structure for this group
        /// </summary>
        internal CollisionStructure structure { get; private set; }

        /// <summary>
        /// Creates a collision group with the given id, name, and collision structure.
        /// </summary>
        /// <param name="id">The unique id for this collision group</param>
        /// <param name="name">The name for this collision group</param>
        /// <param name="structure">The backing collision structure for this collision group</param>
        internal CollisionGroup(uint id, string name, CollisionStructure structure)
        {
            this.GROUP_ID = id;
            this.name = name;
            this.structure = structure;
        }
    }
}
