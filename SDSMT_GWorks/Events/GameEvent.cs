using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDSMTGDT.GWorks.Events
{
    /// <summary>
    /// Delegate/ Multicast function pointer to GameEvent Listeners
    /// </summary>
    /// <typeparam name="EventInfoType">The type of Game Event Info the function will take</typeparam>
    /// <param name="sender">The object who fired the event, usually a publisher</param>
    /// <param name="eventInfo">The information assciated with the event</param>
    public delegate void GameEvent<EventInfoType>
        (object sender, EventInfoType eventInfo) where EventInfoType : GameEventInfo;
}
