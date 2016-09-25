using SDSMTGDT.GWorks.GameStates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace SDSMTGDT.GWorks.Events
{
    /// <summary>
    /// This class handles storing and firing events.
    /// </summary>
    public class EventManager : UpdateListener
    {
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
            private GameEventActions<T> eventActions;
            internal DelayedEvent(EventManager manager, object sender, EventID<T> eventID, T eventInfo)
            {
                this.manager = manager;
                this.sender = sender;
                this.eventID = eventID;
                this.eventInfo = eventInfo;
                this.eventActions = manager.getEventActions(eventID);
            }
            
            public void fireEvent()
            {
                eventActions.listeners?.Invoke(sender, eventInfo);
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
        private Dictionary<uint, IGameEventAction> eventMap;

        internal GameEventActions<T> getEventActions<T>(EventID<T> eventID) where T : GameEventInfo
        {
            return (GameEventActions<T>)eventMap[eventID.id];
        }

        //Allows for delayed execution of events
        private LinkedList<IDelayedEvent> delayedEvents;

        //Allows for changing game states etc after they run
        private Queue<Action> delayedActions;

        //Creates eventIDs
        private EventIDFactory eventIDFactory;

        /// <summary>
        /// Constructor for an EventManager.
        /// </summary>
        public EventManager()
        {
            eventMap = new Dictionary<uint, IGameEventAction>();
            delayedEvents = new LinkedList<IDelayedEvent>();
            delayedActions = new Queue<Action>();
            eventIDFactory = new EventIDFactory();
        }

        /// <summary>
        /// Creates and registers a new eventID into the eventMap
        /// </summary>
        /// <typeparam name="T">type of event info being handled</typeparam>
        /// <param name="description">description of the event</param>
        /// <returns>returns an object with the id and description
        /// attached.</returns>
        internal EventID<T> registerEvent<T>(string description) where T : GameEventInfo
        {
            EventID<T> eventID = eventIDFactory.createEventID<T>(description);
            eventMap.Add(eventID.id, new GameEventActions<T>());
            return eventID;
        }

        /// <summary>
        /// Removes an eventID from the event map when it is no longer needed
        /// </summary>
        /// <typeparam name="T">A type of GameEventInfo associated with the EventID</typeparam>
        /// <param name="eventID">The eventID to remove from the map</param>
        internal void removeEvent<T>(EventID<T> eventID) where T : GameEventInfo
        {
            eventMap.Remove(eventID.id);
            var node = delayedEvents.First;
            while (node != null)
            {
                var nextNode = node.Next;
                if (node.Value is DelayedEvent<T> && ((DelayedEvent<T>)node.Value).eventID.id == eventID.id)
                {
                    delayedEvents.Remove(node);
                }
                node = nextNode;
            }
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
            delayedEvents.AddLast(new DelayedEvent<T>(this, sender, eventID, eventInfo));
        }

        public void queueAction(Action a)
        {
            delayedActions.Enqueue(a);
        }

        /// <summary>
        /// Handles firing delayed events.
        /// </summary>
        public void update(GameTime gameTime)
        {
            while (delayedEvents.Count != 0)
            {
                delayedEvents.First().fireEvent();
                delayedEvents.RemoveFirst();
            }

            while (delayedActions.Count != 0)
            {
                //Dequeue returns a function 
                //The second set of parens call the function
                delayedActions.Dequeue()();
            }
        }

        /// <summary>
        /// Returns the amount of registered events in the eventMap.
        /// </summary>
        /// <returns>The amount of currently active events</returns>
        public int getEventCount()
        {
            return eventMap.Count;
        }
    }
}
