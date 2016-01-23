using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDSMTGDT.Gworks.Events
{
    // Defines the method signature for consuming a game event
    public delegate void GameEvent<eventType>(object sender, eventType eventInfo);
    
    // An interface which all information passed through game events should conform to
    public interface GameEventInfo
    {
    }

    // Descending classes must implement a publish method which calls fireEvent
    // TODO: add async events
    public abstract class GameEventPublisher<T> where T : GameEventInfo
    {
        // A multicast delegate for subscribers
        private GameEvent<T> subscribers;
        private GameEvent<T> asyncSubscribers;
        
        // Sends GameEventInfo to subscribers
        protected void fireEvent(T eventInfo)
        {
            asyncSubscribers.BeginInvoke(this, eventInfo, null, null);
            subscribers(this, eventInfo);
        }

        public void addListener(GameEvent<T> listener)
        {
            subscribers += listener;
        }

        public void removeListener(GameEvent<T> listener)
        {
            subscribers -= listener;
        }
    
        public void addAsyncListener(GameEvent<T> listener)
        {
            asyncSubscribers += listener;
        }

        public void removeAsyncListener(GameEvent<T> listener)
        {
            asyncSubscribers -= listener;
        }

        //Allow for oopy design
        public void addSubscriber(GameEventSubscriber<T> subscriber)
        {
            subscribers += subscriber.gameEventRecieved;
        }

        public void removeSubscriber(GameEventSubscriber<T> subscriber)
        {
            subscribers -= subscriber.gameEventRecieved;
        }

        public void addAsyncSubscriber(GameEventSubscriber<T> subscriber)
        {
            asyncSubscribers += subscriber.gameEventRecieved;
        }

        public void removeAsyncSubscriber(GameEventSubscriber<T> subscriber)
        {
            asyncSubscribers -= subscriber.gameEventRecieved;
        }
    }


    public interface GameEventSubscriber<T> where T : GameEventInfo
    {
        void gameEventRecieved(object source, T eventInfo);
    }
}
