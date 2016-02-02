using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDSMTGDT.GWorks.Events
{
    //Stores information on an event
    //Acts as a connector between publishers and subscribers in the manager
    //Only created by the EventType factory in the EventManager
    public class EventType<T> where T : GameEventInfo
    {
        public uint id { get; private set; }
        public string description { get; private set; }
        internal EventType(uint id, string description)
        {
            this.id = id;
            this.description = description;
        }
    }
}
