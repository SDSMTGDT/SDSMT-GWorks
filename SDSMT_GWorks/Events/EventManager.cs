using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
namespace SDSMTGDT.Gworks.Events
{
    public class EventManager
    {
        //Interface to allow EventActions to be stored in the dictionary
        private interface IEventActions { }

        //Stores the subscribers and listeners associated with an event
        private class EventActions<T> : IEventActions where T : GameEventInfo
        {
            internal GameEvent<T> listeners;
            internal GameEvent<T> asyncListeners;
        }

        //Interface to allow DelayedEvents to be stored in a queue
        private interface IDelayedEvent { void fireEvent(); }
        //Provide a closure for capturing the variables needed to fire a delayed event
        private class DelayedEvent<T> : IDelayedEvent where T : GameEventInfo
        {
            private EventManager manager;
            internal object sender { get; private set; }
            internal EventType<T> eventType { get; private set; }
            internal T eventInfo { get; private set; }
            internal DelayedEvent(EventManager manager, object sender, EventType<T> eventType, T eventInfo)
            {
                this.manager = manager;
                this.sender = sender;
                this.eventType = eventType;
                this.eventInfo = eventInfo;
            }
            
            public void fireEvent()
            {
                manager.fireEvent(sender, eventType, eventInfo);
            }
        }

        //Creates an event type, making sure not to repeat ids
        private class EventTypeFactory
        {
            uint eventIdCounter = 0;
            internal EventType<T> createEventType<T>(string description) where T : GameEventInfo
            {
                return new EventType<T>(eventIdCounter++, description);
            }
        }

        //Maps event type ids to subscribers
        private Dictionary<uint, IEventActions> eventMap;

        //Allows for delayed execution of events
        private Queue<IDelayedEvent> delayedEvents;

        //Creates event types
        private EventTypeFactory eventTypeFactory;

        public EventManager()
        {
            eventMap = new Dictionary<uint, IEventActions>();
            delayedEvents = new Queue<IDelayedEvent>();
            eventTypeFactory = new EventTypeFactory();
        }

        //Creates and registers a new event type into the eventMap
        internal EventType<T> registerEventType<T>(string description) where T : GameEventInfo
        {
            EventType<T> eventType = eventTypeFactory.createEventType<T>(description);
            eventMap.Add(eventType.id, new EventActions<T>());
            return eventType;
        }

        //Registers an event type with an event listener
        public void registerEventListener<T>(EventType<T> eventType, GameEvent<T> eventListener)
            where T : GameEventInfo
        {
            ((EventActions<T>)eventMap[eventType.id]).listeners += eventListener;
        }

        //Registers an event type with an asynchronous event listener
        public void registerAsyncEventListener<T>(EventType<T> eventType, GameEvent<T> eventListener)
            where T : GameEventInfo
        {
            ((EventActions<T>)eventMap[eventType.id]).asyncListeners += eventListener;
        }

        //Unregisters an event listener from an event type
        public void unregisterEventListener<T>(EventType<T> eventType, GameEvent<T> eventListener)
            where T : GameEventInfo
        {
            ((EventActions<T>)eventMap[eventType.id]).listeners -= eventListener;
        }

        //Unregisters a threaded event listener from an event type
        public void unregisterAsyncEventListener<T>(EventType<T> eventType, GameEvent<T> eventListener)
            where T : GameEventInfo
        {
            ((EventActions<T>)eventMap[eventType.id]).asyncListeners -= eventListener;
        }

        //Registers an event type with an event subscriber
        public void registerEventSubscriber<T>(EventType<T> eventType, GameEventSubscriber<T> eventSubscriber)
            where T : GameEventInfo
        {
            ((EventActions<T>)eventMap[eventType.id]).listeners += eventSubscriber.gameEventRecieved;
        }

        //Registers an event type with an asynchronous event subscriber
        public void registerAsyncEventSubscriber<T>(EventType<T> eventType, GameEventSubscriber<T> eventSubscriber)
            where T : GameEventInfo
        {
            ((EventActions<T>)eventMap[eventType.id]).asyncListeners += eventSubscriber.gameEventRecieved;
        }

        //Unregisters an event subscriber from an event type
        public void unregisterEventSubscriber<T>(EventType<T> eventType, GameEventSubscriber<T> eventSubscriber)
            where T : GameEventInfo
        {
            ((EventActions<T>)eventMap[eventType.id]).listeners -= eventSubscriber.gameEventRecieved;
        }

        //Unregisters a threaded event subscriber from an event type
        public void unregisterAsyncEventSubscriber<T>(EventType<T> eventType, GameEventSubscriber<T> eventSubscriber)
            where T : GameEventInfo
        {
            ((EventActions<T>)eventMap[eventType.id]).asyncListeners -= eventSubscriber.gameEventRecieved;
        }

        //Pushes an event info object through to the listeners and subscribers associated with the event type
        internal void fireEvent<T>(object sender, EventType<T> eventType, T eventInfo)
            where T : GameEventInfo
        {
            EventActions<T> actions = (EventActions<T>)eventMap[eventType.id];
            actions?.listeners?.Invoke(sender, eventInfo);
            actions?.asyncListeners?.BeginInvoke(sender, eventInfo, null, null);
        }

        //Queues an event for later execution
        internal void queueEvent<T>(object sender, EventType<T> eventType, T eventInfo)
            where T : GameEventInfo
        {
            delayedEvents.Enqueue(new DelayedEvent<T>(this, sender, eventType, eventInfo));
        }

        public void processQueuedEvents()
        {
            while (delayedEvents.Count != 0)
            {
                delayedEvents.Dequeue().fireEvent();
            }
        }
    }
}
