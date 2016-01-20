using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDSMTGDT.Gworks.Events
{
    public class GenericGameEventInfo<T1> : GameEventInfo
    {
        public GenericGameEventInfo(T1 item1)
        {
            this.item1 = item1;
        }

        public T1 item1 { get; set; }
    }

    public class GenericGameEventPublisher<T1> : GameEventPublisher<GenericGameEventInfo<T1>>
    {
        public void publish(T1 item1)
        {
            GenericGameEventInfo<T1> info = new GenericGameEventInfo<T1>(item1);
            fireEvent(info);
        }
    }

    public interface GenericGameEventSubscriber<T1> : GameEventSubscriber<GenericGameEventInfo<T1>> { }
}
