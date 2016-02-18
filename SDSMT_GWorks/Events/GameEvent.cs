using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDSMTGDT.GWorks.Events
{
    // Defines the method signature for consuming a game event
    /// <summary>
    /// Delegate/ Multicast function pointer to GameEvent Listeners
    /// </summary>
    /// <typeparam name="eventInfoType">The type of Game Event Info the function will take</typeparam>
    /// <param name="sender">The object who fired the event, usually a publisher</param>
    /// <param name="eventInfo">The information assciated with the event</param>
    public delegate void GameEvent<eventInfoType>
        (object sender, eventInfoType eventInfo) where eventInfoType : GameEventInfo;
}
