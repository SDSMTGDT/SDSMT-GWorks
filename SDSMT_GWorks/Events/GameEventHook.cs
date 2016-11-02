using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDSMTGDT.GWorks.Events
{
    /// <summary>
    /// Used to allow subscribers and publishers to register for an event.
    /// Normally a publisher would be used for this, however there are times
    /// when we wish to allow other pieces of code to listen to an event
    /// without exposing the publisher.
    /// </summary>
    public class GameEventHook<T> : IDisposable where T : GameEventInfo
    {
        /// <summary>
        /// The event id which identifies the sink for this publisher
        /// </summary>
        public EventID<T> EVENT_ID { get; }

        /// <summary>
        /// The manager to connect the publisher with the subscribers
        /// </summary>
        protected EventManager Manager;

        public GameEventHook(EventManager manager, EventID<T> eventID)
        {
            Manager = manager;
            EVENT_ID = eventID;
        }

        /// <summary>
        /// Registers an event listener with this publisher
        /// </summary>
        /// <param name="eventListener">A function to call when the event fires</param>
        public void RegisterEventListener(GameEvent<T> eventListener)
        {
            Manager.GetEventActions(EVENT_ID).Listeners += eventListener;
        }

        /// <summary>
        /// Registers a threaded event listener with this publisher
        /// </summary>
        /// <param name="eventListener">A function to call when the event fires</param>
        public void RegisterAsyncEventListener(GameEvent<T> eventListener)
        {
            Manager.GetEventActions(EVENT_ID).AsyncListeners += eventListener;
        }

        /// <summary>
        /// Removes an event listener corresponding to this publisher
        /// </summary>
        /// <param name="eventListener">A function currently being called when the event fires</param>
        public void UnregisterEventListener(GameEvent<T> eventListener)
        {
            Manager.GetEventActions(EVENT_ID).Listeners -= eventListener;
        }

        /// <summary>
        /// Removes a threaded event listener from the manager corresponding to this publisher
        /// </summary>
        /// <param name="eventListener">A function currently being called when the event fires</param>
        public void UnregisterAsyncEventListener(GameEvent<T> eventListener)
        {
            Manager.GetEventActions(EVENT_ID).AsyncListeners -= eventListener;
        }

        /// <summary>
        /// Registers an object oriented event listener with this publisher through the manager
        /// </summary>
        /// <param name="eventSubscriber">An object containing a method to call when the event fires</param>
        public void RegisterEventSubscriber(GameEventSubscriber<T> eventSubscriber)
        {
            RegisterEventListener(eventSubscriber.GameEventRecieved);
        }

        /// <summary>
        /// Registers an object oriented threaded event listener with this publisher through the manager
        /// </summary>
        /// <param name="eventSubscriber">An object containing a method to call when the event fires</param>
        public void RegisterAsyncEventSubscriber(GameEventSubscriber<T> eventSubscriber)
        {
            RegisterAsyncEventListener(eventSubscriber.GameEventRecieved);
        }

        /// <summary>
        /// Removes an object oriented event listener from the manager corresponding to this publisher
        /// </summary>
        /// <param name="eventSubscriber">An object containing a method which is being called when the event fires</param>
        public void UnregisterEventSubscriber(GameEventSubscriber<T> eventSubscriber)
        {
            UnregisterEventListener(eventSubscriber.GameEventRecieved);
        }

        /// <summary>
        /// Removes an object oriented threaded event listener from the manager corresponding to this publisher
        /// </summary>
        /// <param name="eventSubscriber">An object containing a method which is being called when the event fires</param>
        public void UnregisterAsyncEventSubscriber(GameEventSubscriber<T> eventSubscriber)
        {
            UnregisterAsyncEventListener(eventSubscriber.GameEventRecieved);
        }

        public virtual void Dispose()
        {
            Manager.RemoveEvent(EVENT_ID);
            //this.EVENT_ID = null;
            //We do not set the event ID to null
            //if fire is called once this is disposed
            //the event manager will simply not do anything
            //due to the checks that are done when the eventid
            //is used to access the dictionary
        }
    }
}
