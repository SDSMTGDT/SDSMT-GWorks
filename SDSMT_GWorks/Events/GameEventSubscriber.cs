using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDSMTGDT.GWorks.Events
{
    /// <summary>
    /// Provides an object oriented way to deal with event listeners
    /// </summary>
    /// <typeparam name="T">The type of GameEventInfo to handle</typeparam>
    public interface GameEventSubscriber<T> where T : GameEventInfo
    {
        void GameEventRecieved(object source, T eventInfo);
    }
}
