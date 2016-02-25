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
    public abstract class GameEventPublisher<T> : IDisposable where T : GameEventInfo
    {
        /// <summary>
        /// The event id which identifies the sink for this publisher
        /// </summary>
        public EventID<T> EVENT_ID { get; private set; }

        /// <summary>
        /// The manager to connect the publisher with the subscribers
        /// </summary>
        private EventManager manager;

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
        private GameEventPublisher(EventManager manager, EventID<T> eventID)
        {
            this.manager = manager;
            EVENT_ID = eventID;
        }

        //Start: Convenience functions. 
        //Alternatives are manager.**********EventListener(publisher.EVENT_ID, eventListener) etc

        /// <summary>
        /// Registers an event listener with this publisher through the manager
        /// </summary>
        /// <param name="eventListener">A function to call when the event fires</param>
        public void registerEventListener(GameEvent<T> eventListener)
        {
            manager.registerEventListener(EVENT_ID, eventListener);
        }

        /// <summary>
        /// Registers a threaded event listener with this publisher through the manager
        /// </summary>
        /// <param name="eventListener">A function to call when the event fires</param>
        public void registerAsyncEventListener(GameEvent<T> eventListener)
        {
            manager.registerAsyncEventListener(EVENT_ID, eventListener);
        }

        /// <summary>
        /// Removes an event listener from the manager corresponding to this publisher
        /// </summary>
        /// <param name="eventListener">A function currently being called when the event fires</param>
        public void unregisterEventListener(GameEvent<T> eventListener)
        {
            manager.unregisterEventListener(EVENT_ID, eventListener);
        }

        /// <summary>
        /// Removes a threaded event listener from the manager corresponding to this publisher
        /// </summary>
        /// <param name="eventListener">A function currently being called when the event fires</param>
        public void unregisterAsyncEventListener(GameEvent<T> eventListener)
        {
            manager.unregisterAsyncEventListener(EVENT_ID, eventListener);
        }

        /// <summary>
        /// Registers an object oriented event listener with this publisher through the manager
        /// </summary>
        /// <param name="eventSubscriber">An object containing a method to call when the event fires</param>
        public void registerEventSubscriber( GameEventSubscriber<T> eventSubscriber)
        {
            manager.registerEventListener(EVENT_ID, eventSubscriber.gameEventRecieved);
        }

        /// <summary>
        /// Registers an object oriented threaded event listener with this publisher through the manager
        /// </summary>
        /// <param name="eventSubscriber">An object containing a method to call when the event fires</param>
        public void registerAsyncEventSubscriber(GameEventSubscriber<T> eventSubscriber)
        {
            manager.registerAsyncEventListener(EVENT_ID, eventSubscriber.gameEventRecieved);
        }

        /// <summary>
        /// Removes an object oriented event listener from the manager corresponding to this publisher
        /// </summary>
        /// <param name="eventSubscriber">An object containing a method which is being called when the event fires</param>
        public void unregisterEventSubscriber(GameEventSubscriber<T> eventSubscriber)
        {
            manager.registerEventListener(EVENT_ID, eventSubscriber.gameEventRecieved);
        }

        /// <summary>
        /// Removes an object oriented threaded event listener from the manager corresponding to this publisher
        /// </summary>
        /// <param name="eventSubscriber">An object containing a method which is being called when the event fires</param>
        public void unregisterAsyncEventSubscriber(GameEventSubscriber<T> eventSubscriber)
        {
            manager.unregisterAsyncEventListener(EVENT_ID, eventSubscriber.gameEventRecieved);
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

        public void Dispose()
        {
            manager.removeEvent(EVENT_ID);
            //this.EVENT_ID = null;
            //We do not set the event ID to null
            //if fire is called once this is disposed
            //the event manager will simply not do anything
            //due to the checks that are done when the eventid
            //is used to access the dictionary
        }
    }
}
