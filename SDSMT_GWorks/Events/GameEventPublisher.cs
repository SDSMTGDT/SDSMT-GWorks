using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDSMTGDT.GWorks.Events
{
    /// <summary>
    /// Main class for firing events. Descending classes must implement a publish method which calls fireEvent
    /// </summary>
    /// <typeparam name="T">The type of GameEventInfo to send with the event</typeparam>
    public abstract class GameEventPublisher<T> : GameEventHook<T> where T : GameEventInfo
    {
        /// <summary>
        /// Chaining constructor which registers a new event id
        /// </summary>
        /// <param name="manager">The Event Manager to publish to</param>
        /// <param name="description">A description of the event being published</param>
        public GameEventPublisher(EventManager manager, string description) : 
            this(manager, manager.registerEventID<T>(description))
        {
        }

        //Sets local information
        /// <summary>
        /// Associates an event ID in the Event Manager with this publisher
        /// </summary>
        /// <param name="manager">The Event Manager to publish to</param>
        /// <param name="eventID">An event id to publish to</param>
        private GameEventPublisher(EventManager manager, EventID<T> eventID) :
            base(manager, eventID)
        {
            
        }
        
        /// <summary>
        /// Sends GameEventInfo to subscribers/ listeners via the manager
        /// </summary>
        /// <param name="eventInfo">The event information to send out</param>
        protected void fireEvent(T eventInfo)
        {
            manager.fireEvent(this, EVENT_ID, eventInfo);
        }
        
        /// <summary>
        /// Sends GameEventInfo to subscribers/listeners via the manager. Will be sent at the end of the tick
        /// </summary>
        /// <param name="eventInfo">The event information to send out</param>
        protected void queueEvent(T eventInfo)
        {
            manager.queueEvent(this, EVENT_ID, eventInfo);
        }
    }
}
