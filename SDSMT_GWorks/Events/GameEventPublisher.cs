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
            this(manager, manager.RegisterEvent<T>(description))
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
        /// Sends GameEventInfo to subscribers/ listeners
        /// </summary>
        /// <param name="eventInfo">The event information to send out</param>
        protected void FireEvent(T eventInfo)
        {
            Manager.GetEventActions(EVENT_ID).Listeners?.Invoke(this, eventInfo);
        }

        /// <summary>
        /// Sends GameEventInfo to subscribers/ listeners threaded
        /// </summary>
        /// <param name="eventInfo">The event information to send out</param>
        /// <param name="callback">A callback to call whe the event is finished</param>
        protected void FireAsyncEvent(T eventInfo, AsyncCallback callback)
        {
            Manager.GetEventActions(EVENT_ID).AsyncListeners?.BeginInvoke(this, eventInfo, callback, null);
        }
        
        /// <summary>
        /// Sends GameEventInfo to subscribers/listeners via the manager. Will be sent at the end of the tick
        /// </summary>
        /// <param name="eventInfo">The event information to send out</param>
        protected void QueueEvent(T eventInfo)
        {
            Manager.QueueEvent(this, EVENT_ID, eventInfo);
        }
    }
}
