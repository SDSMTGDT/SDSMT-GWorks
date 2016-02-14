using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDSMTGDT.GWorks.Events
{
    // Descending classes must implement a publish method which calls fireEvent
    public abstract class GameEventPublisher<T> where T : GameEventInfo
    {
        //The event type which identifies the sink for this publisher
        public EventID<T> EVENT_TYPE { get; private set; }

        //The manager to connect the publisher with the subscribers
        private EventManager manager;

        //Chaining constructor which registers a new event type
        public GameEventPublisher(EventManager manager, string description) : 
            this(manager, manager.registerEventID<T>(description))
        {
        }

        //Sets local information
        public GameEventPublisher(EventManager manager, EventID<T> eventType)
        {
            this.manager = manager;
            EVENT_TYPE = eventType;
        }

        //Start: Convenience functions. 
        //Alternatives are manager.**********EventListener(publisher.EVENT_TYPE, eventListener) etc

        //Registers an event listener with this publisher through the manager
        public void registerEventListener(GameEvent<T> eventListener)
        {
            manager.registerEventListener(EVENT_TYPE, eventListener);
        }

        //Registers a threaded event listener with this publisher through the manager
        public void registerAsyncEventListener(GameEvent<T> eventListener)
        {
            manager.registerAsyncEventListener(EVENT_TYPE, eventListener);
        }

        //Removes an event listener from the manager corresponding to this publisher
        public void unregisterEventListener(GameEvent<T> eventListener)
        {
            manager.unregisterEventListener(EVENT_TYPE, eventListener);
        }

        //Removes a threaded event listener from the manager corresponding to this publisher
        public void unregisterAsyncEventListener(GameEvent<T> eventListener)
        {
            manager.unregisterAsyncEventListener(EVENT_TYPE, eventListener);
        }

        //Registers an object oriented event listener with this publisher through the manager
        public void registerEventSubscriber( GameEventSubscriber<T> eventSubscriber)
        {
            manager.registerEventListener(EVENT_TYPE, eventSubscriber.gameEventRecieved);
        }

        //Registers an object oriented threaded event listener with this publisher through the manager
        public void registerAsyncEventSubscriber(GameEventSubscriber<T> eventSubscriber)
        {
            manager.registerAsyncEventListener(EVENT_TYPE, eventSubscriber.gameEventRecieved);
        }

        //Removes an object oriented event listener from the manager corresponding to this publisher
        public void unregisterEventSubscriber(GameEventSubscriber<T> eventSubscriber)
        {
            manager.registerEventListener(EVENT_TYPE, eventSubscriber.gameEventRecieved);
        }

        //Removes an object oriented threaded event listener from the manager corresponding to this publisher
        public void unregisterAsyncEventSubscriber(GameEventSubscriber<T> eventSubscriber)
        {
            manager.unregisterAsyncEventListener(EVENT_TYPE, eventSubscriber.gameEventRecieved);
        }

        // Sends GameEventInfo to subscribers via the manager
        protected void fireEvent(T eventInfo)
        {
            manager.fireEvent(this, EVENT_TYPE, eventInfo);
        }
        
        protected void queueEvent(T eventInfo)
        {
            manager.queueEvent(this, EVENT_TYPE, eventInfo);
        }
    }
}
