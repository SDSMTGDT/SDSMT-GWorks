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
        private interface IDelayedEvent { void FireEvent(); }
        
        /// <summary>
        /// Provide a closure for capturing the variables needed to fire a 
        /// delayed event
        /// </summary>
        /// <typeparam name="T">type of the event info being handled</typeparam>
        private class DelayedEvent<T> : IDelayedEvent where T : GameEventInfo
        {
            internal EventID<T> EventID { get; }
            private readonly object sender;
            private readonly T eventInfo;
            private readonly GameEventActions<T> eventActions;
            internal DelayedEvent(EventManager manager, object sender, EventID<T> eventID, T eventInfo)
            {
                this.sender = sender;
                this.EventID = eventID;
                this.eventInfo = eventInfo;
                this.eventActions = manager.GetEventActions(eventID);
            }
            
            public void FireEvent()
            {
                eventActions.Listeners?.Invoke(sender, eventInfo);
            }
        }

        /// <summary>
        /// Creates an eventID, making sure not to repeat ids
        /// </summary>
        private class EventIDFactory
        {
            uint eventIdCounter = 0;
            internal EventID<T> CreateEventId<T>(string description) where T : GameEventInfo
            {
                return new EventID<T>(eventIdCounter++, description);
            }
        }

        //Maps eventID ids to subscribers
        private readonly Dictionary<uint, IGameEventAction> eventMap;

        internal GameEventActions<T> GetEventActions<T>(EventID<T> eventID) where T : GameEventInfo
        {
            return (GameEventActions<T>)eventMap[eventID.ID];
        }

        //Allows for delayed execution of events
        private readonly LinkedList<IDelayedEvent> delayedEvents;

        //Allows for changing game states etc after they run
        private readonly Queue<Action> delayedActions;

        //Creates eventIDs
        private readonly EventIDFactory eventIDFactory;

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
        internal EventID<T> RegisterEvent<T>(string description) where T : GameEventInfo
        {
            EventID<T> eventID = eventIDFactory.CreateEventId<T>(description);
            eventMap.Add(eventID.ID, new GameEventActions<T>());
            return eventID;
        }

        /// <summary>
        /// Removes an eventID from the event map when it is no longer needed
        /// </summary>
        /// <typeparam name="T">A type of GameEventInfo associated with the EventID</typeparam>
        /// <param name="eventID">The eventID to remove from the map</param>
        internal void RemoveEvent<T>(EventID<T> eventID) where T : GameEventInfo
        {
            eventMap.Remove(eventID.ID);
            var node = delayedEvents.First;
            while (node != null)
            {
                var nextNode = node.Next;
                if (node.Value is DelayedEvent<T> && ((DelayedEvent<T>)node.Value).EventID.ID == eventID.ID)
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
        internal void QueueEvent<T>(object sender, EventID<T> eventID, T eventInfo)
            where T : GameEventInfo
        {
            delayedEvents.AddLast(new DelayedEvent<T>(this, sender, eventID, eventInfo));
        }

        public void QueueAction(Action a)
        {
            delayedActions.Enqueue(a);
        }

        /// <summary>
        /// Handles firing delayed events.
        /// </summary>
        public void Update(GameTime gameTime)
        {
            while (delayedEvents.Count != 0)
            {
                delayedEvents.First().FireEvent();
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
        public int GetEventCount()
        {
            return eventMap.Count;
        }
    }
}
