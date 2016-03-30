using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDSMTGDT.GWorks.Events
{
    /// <summary>
    /// Interface to allow EventActions to be stored in the dictionary
    /// </summary>
    internal interface IGameEventAction { }

    /// <summary>
    /// Stores the subscribers and listeners associated with an event
    /// </summary>
    /// <typeparam name="T">type of the event info being handled</typeparam>
    internal class GameEventActions<T> : IGameEventAction where T : GameEventInfo
    {
        internal GameEvent<T> listeners;
        internal GameEvent<T> asyncListeners;
    }
}
