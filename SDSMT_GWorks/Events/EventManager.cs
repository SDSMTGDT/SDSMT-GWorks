using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
namespace SDSMTGDT.GWorks.Events
{
    /// <summary>
    /// This class handles storing and firing events.
    /// </summary>
    public class EventManager
    {
        /// <summary>
        /// Interface to allow EventActions to be stored in the dictionary
        /// </summary>
        private interface IEventActions { }

        /// <summary>
        /// Stores the subscribers and listeners associated with an event
        /// </summary>
        /// <typeparam name="T">type of the event info being handled</typeparam>
        private class EventActions<T> : IEventActions where T : GameEventInfo
        {
            internal GameEvent<T> listeners;
            internal GameEvent<T> asyncListeners;
        }

        /// <summary>
        /// Interface to allow DelayedEvents to be stored in a queue
        /// </summary>
        private interface IDelayedEvent { void fireEvent(); }
        /// <summary>
        /// Provide a closure for capturing the variables needed to fire a 
        /// delayed event
        /// </summary>
        /// <typeparam name="T">type of the event info being handled</typeparam>
        private class DelayedEvent<T> : IDelayedEvent where T : GameEventInfo
        {
            private EventManager manager;
            internal object sender { get; private set; }
            internal EventID<T> eventID { get; private set; }
            internal T eventInfo { get; private set; }
            internal DelayedEvent(EventManager manager, object sender, EventID<T> eventID, T eventInfo)
            {
                this.manager = manager;
                this.sender = sender;
                this.eventID = eventID;
                this.eventInfo = eventInfo;
            }
            
            public void fireEvent()
            {
                manager.fireEvent(sender, eventID, eventInfo);
            }
        }

        /// <summary>
        /// Creates an eventID, making sure not to repeat ids
        /// </summary>
        private class EventIDFactory
        {
            uint eventIdCounter = 0;
            internal EventID<T> createEventID<T>(string description) where T : GameEventInfo
            {
                return new EventID<T>(eventIdCounter++, description);
            }
        }

        //Maps eventID ids to subscribers
        private Dictionary<uint, IEventActions> eventMap;

        //Allows for delayed execution of events
        private Queue<IDelayedEvent> delayedEvents;

        //Creates eventIDs
        private EventIDFactory eventIDFactory;

        /// <summary>
        /// Constructor for an EventManager.
        /// </summary>
        public EventManager()
        {
            eventMap = new Dictionary<uint, IEventActions>();
            delayedEvents = new Queue<IDelayedEvent>();
            eventIDFactory = new EventIDFactory();
        }

        /// <summary>
        /// Creates and registers a new eventID into the eventMap
        /// </summary>
        /// <typeparam name="T">type of event info being handled</typeparam>
        /// <param name="description">description of the event</param>
        /// <returns>returns an object with the id and description
        /// attached.</returns>
        internal EventID<T> registerEventID<T>(string description) where T : GameEventInfo
        {
            EventID<T> eventID = eventIDFactory.createEventID<T>(description);
            eventMap.Add(eventID.id, new EventActions<T>());
            return eventID;
        }

        /// <summary>
        /// Registers an eventID with an event listener
        /// </summary>
        /// <typeparam name="T">type of event info being handled</typeparam>
        /// <param name="eventID">contains the id and description
        /// of the event</param>
        /// <param name="eventListener">what will be called as a result of
        /// the event firing.</param>
        public void registerEventListener<T>(EventID<T> eventID, GameEvent<T> eventListener)
            where T : GameEventInfo
        {
            ((EventActions<T>)eventMap[eventID.id]).listeners += eventListener;
        }

        /// <summary>
        /// Registers an event id with an asynchronous event listener
        /// </summary>
        /// <typeparam name="T">type of event info being handled</typeparam>
        /// <param name="eventID">Contains the id and description of the
        /// event</param>
        /// <param name="eventListener">the functions that will be called by
        /// the event</param>
        public void registerAsyncEventListener<T>(EventID<T> eventID, GameEvent<T> eventListener)
            where T : GameEventInfo
        {
            ((EventActions<T>)eventMap[eventID.id]).asyncListeners += eventListener;
        }

        /// <summary>
        /// Unregisters an event listener from an eventID
        /// </summary>
        /// <typeparam name="T">Type of event info being handled</typeparam>
        /// <param name="eventID">Contains the id and description of the
        /// event</param>
        /// <param name="eventListener">the functions that will be called by
        /// the event</param>
        public void unregisterEventListener<T>(EventID<T> eventID, GameEvent<T> eventListener)
            where T : GameEventInfo
        {
            ((EventActions<T>)eventMap[eventID.id]).listeners -= eventListener;
        }

        /// <summary>
        /// Unregisters a threaded event listener from an eventID
        /// </summary>
        /// <typeparam name="T">Type of event info being handled</typeparam>
        /// <param name="eventID">Contains the id and description of the
        /// event</param>
        /// <param name="eventListener">the functions that will be called when
        /// the event is fired</param>
        public void unregisterAsyncEventListener<T>(EventID<T> eventID, GameEvent<T> eventListener)
            where T : GameEventInfo
        {
            ((EventActions<T>)eventMap[eventID.id]).asyncListeners -= eventListener;
        }

        /// <summary>
        /// Registers an eventID with an event subscriber
        /// </summary>
        /// <typeparam name="T">Type of event info being handled</typeparam>
        /// <param name="eventID">Contains the id and description of the
        /// event</param>
        /// <param name="eventSubscriber">An interface that contains an
        /// event listener.</param>
        public void registerEventSubscriber<T>(EventID<T> eventID, GameEventSubscriber<T> eventSubscriber)
            where T : GameEventInfo
        {
            ((EventActions<T>)eventMap[eventID.id]).listeners += eventSubscriber.gameEventRecieved;
        }

        /// <summary>
        /// Registers an eventID with an asynchronous event subscriber
        /// </summary>
        /// <typeparam name="T">Type of event info being handled</typeparam>
        /// <param name="eventID">Contains the id and description of the
        /// event</param>
        /// <param name="eventSubscriber">An interface that contains an event
        /// listener</param>
        public void registerAsyncEventSubscriber<T>(EventID<T> eventID, GameEventSubscriber<T> eventSubscriber)
            where T : GameEventInfo
        {
            ((EventActions<T>)eventMap[eventID.id]).asyncListeners += eventSubscriber.gameEventRecieved;
        }

        /// <summary>
        /// Unregisters an event subscriber from an eventID
        /// </summary>
        /// <typeparam name="T">Type of event info being handled</typeparam>
        /// <param name="eventID">Contains the id and description of the
        /// event</param>
        /// <param name="eventSubscriber">An interface that contains an event
        /// listener</param>
        public void unregisterEventSubscriber<T>(EventID<T> eventID, GameEventSubscriber<T> eventSubscriber)
            where T : GameEventInfo
        {
            ((EventActions<T>)eventMap[eventID.id]).listeners -= eventSubscriber.gameEventRecieved;
        }

        /// <summary>
        /// Unregisters a threaded event subscriber from an eventID
        /// </summary>
        /// <typeparam name="T">Type of event info being handled</typeparam>
        /// <param name="eventID">Contains the id and description of the
        /// event</param>
        /// <param name="eventSubscriber">An interface that contains an event
        /// listener</param>
        public void unregisterAsyncEventSubscriber<T>(EventID<T> eventID, GameEventSubscriber<T> eventSubscriber)
            where T : GameEventInfo
        {
            ((EventActions<T>)eventMap[eventID.id]).asyncListeners -= eventSubscriber.gameEventRecieved;
        }

        /// <summary>
        /// Pushes an event info object through to the listeners and 
        /// subscribers associated with the eventID
        /// </summary>
        /// <typeparam name="T">Type of event info being handled</typeparam>
        /// <param name="sender">Object that fired the event</param>
        /// <param name="eventID">Contains the id and description of the
        /// event</param>
        /// <param name="eventInfo">The info being sent</param>
        internal void fireEvent<T>(object sender, EventID<T> eventID, T eventInfo)
            where T : GameEventInfo
        {
            EventActions<T> actions = (EventActions<T>)eventMap[eventID.id];
            actions?.listeners?.Invoke(sender, eventInfo);
            actions?.asyncListeners?.BeginInvoke(sender, eventInfo, null, null);
        }

        /// <summary>
        /// Queues an event for later execution
        /// </summary>
        /// <typeparam name="T">Type of event info being handled</typeparam>
        /// <param name="sender">Object that fired the event</param>
        /// <param name="eventID">Contains the id and description of the
        /// event</param>
        /// <param name="eventInfo">The info being sent</param>
        internal void queueEvent<T>(object sender, EventID<T> eventID, T eventInfo)
            where T : GameEventInfo
        {
            delayedEvents.Enqueue(new DelayedEvent<T>(this, sender, eventID, eventInfo));
        }

        /// <summary>
        /// Handles firing delayed events.
        /// </summary>
        public void processQueuedEvents()
        {
            while (delayedEvents.Count != 0)
            {
                delayedEvents.Dequeue().fireEvent();
            }
        }
    }
}
