﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDSMTGDT.GWorks.Events.Generic
{
    public class GenericGameEventInfo<T1, T2> : GameEventInfo
    {
        public GenericGameEventInfo(T1 item1, T2 item2)
        {
            this.item1 = item1;
            this.item2 = item2;
        }

        public T1 item1 { get; set; }
        public T2 item2 { get; set; }
    }

    public class GenericGameEventPublisher<T1, T2> : GameEventPublisher<GenericGameEventInfo<T1, T2>>
    {
        public GenericGameEventPublisher(EventManager manager, string description)
            : base(manager, description)
        {

        }

        public void publish(T1 item1, T2 item2)
        {
            var info = new GenericGameEventInfo<T1, T2>(item1, item2);
            fireEvent(info);
        }

        public void publishDelayedEvent(T1 item1, T2 item2)
        {
            GenericGameEventInfo<T1, T2> info = new GenericGameEventInfo<T1, T2>(item1, item2);
            queueEvent(info);
        }
    }

    public interface GenericGameEventConsumer<T1, T2> : GameEventSubscriber<GenericGameEventInfo<T1, T2>> { }
}
