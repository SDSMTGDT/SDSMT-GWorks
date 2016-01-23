using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDSMTGDT.Gworks.Events
{
    // Defines the method signature for consuming a game event
    public delegate void GameEvent<eventInfoType>
        (object sender, eventInfoType eventInfo) where eventInfoType : GameEventInfo;
}
