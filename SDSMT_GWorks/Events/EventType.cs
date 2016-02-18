using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDSMTGDT.GWorks.Events
{
    /// <summary>
    /// Stores information on an event
    /// Acts as a connector between publishers and subscribers in the manager
    /// Only created by the EventID factory in the EventManager
    /// </summary>
    /// <typeparam name="T">Type of class info being handled</typeparam>
    public class EventID<T> where T : GameEventInfo
    {
        /// <summary>
        /// 
        /// </summary>
        public uint id { get; private set; }
        public string description { get; private set; }
        internal EventID(uint id, string description)
        {
            this.id = id;
            this.description = description;
        }
    }
}
