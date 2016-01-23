using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDSMTGDT.Gworks.Events
{
    //Provides an object oriented way to deal with event listeners
    public interface GameEventSubscriber<T> where T : GameEventInfo
    {
        void gameEventRecieved(object source, T eventInfo);
    }
}
