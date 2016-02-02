using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDSMTGDT.GWorks.Events
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
        public GenericGameEventPublisher(EventManager manager, string description) 
            : base(manager, description)
        {

        }

        public void publish(T1 item1)
        {
            GenericGameEventInfo<T1> info = new GenericGameEventInfo<T1>(item1);
            fireEvent(info);
        }

        public void publishDelayedEvent(T1 item1)
        {
            GenericGameEventInfo<T1> info = new GenericGameEventInfo<T1>(item1);
            queueEvent(info);
        }
    }

    public interface GenericGameEventConsumer<T1> : GameEventSubscriber<GenericGameEventInfo<T1>> { }
}
