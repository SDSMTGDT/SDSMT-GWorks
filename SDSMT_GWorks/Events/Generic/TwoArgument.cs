using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDSMTGDT.GWorks.Events.Generic
{
    /// <summary>
    /// This class defines an event with two pieces of information packaged.
    /// </summary>
    /// <typeparam name="T1">The type of the first piece of information</typeparam>
    /// <typeparam name="T2">The type of the second piece of information</typeparam>
    public class GenericGameEventInfo<T1, T2> : GameEventInfo
    {
        /// <summary>
        /// Constructor for an event with two pieces of information.
        /// </summary>
        /// <param name="item1">The first piece of information packaged</param>
        /// <param name="item2">The second piece of information packaged</param>
        public GenericGameEventInfo(T1 item1, T2 item2)
        {
            this.item1 = item1;
            this.item2 = item2;
        }

        /// <summary>
        /// Container for the first piece of information.
        /// </summary>
        public T1 item1 { get; set; }
        /// <summary>
        /// Container for the second piece of information.
        /// </summary>
        public T2 item2 { get; set; }
    }

    /// <summary>
    /// This class defines a game event publisher for events with two pieces of
    /// information. this class packages the information to be sent with our
    /// event.
    /// </summary>
    /// <typeparam name="T1">The type of the first piece of information.</typeparam>
    /// <typeparam name="T2">The type of the second piece of information.</typeparam>
    public class GenericGameEventPublisher<T1, T2> : GameEventPublisher<GenericGameEventInfo<T1, T2>>
    {
        /// <summary>
        /// Constructor for a two info event publisher.
        /// </summary>
        /// <param name="manager">Event manager that will handle the event</param>
        /// <param name="description">Description of the event</param>
        public GenericGameEventPublisher(EventManager manager, string description)
            : base(manager, description)
        {

        }

        /// <summary>
        /// Packages the two pieces of information provided into an event,
        /// and then fires that event.
        /// </summary>
        /// <param name="item1">First piece of information to be packaged</param>
        /// <param name="item2">Second piece of information to be packaged</param>
        public void publish(T1 item1, T2 item2)
        {
            var info = new GenericGameEventInfo<T1, T2>(item1, item2);
            fireEvent(info);
        }
        
        /// <summary>
        /// Packages the two pieces of information provided into an event in
        /// such a way as to allow it to fire after a delay.
        /// </summary>
        /// <param name="item1">First piece of information to be packaged</param>
        /// <param name="item2">Second piece of information to be packaged</param>
        public void publishDelayedEvent(T1 item1, T2 item2)
        {
            GenericGameEventInfo<T1, T2> info = new GenericGameEventInfo<T1, T2>(item1, item2);
            queueEvent(info);
        }
    }

    /// <summary>
    /// The interface for those who need information from the event.
    /// </summary>
    /// <typeparam name="T1">The type of the first piece of information.</typeparam>
    /// <typeparam name="T2">The type of the second piece of information.</typeparam>
    public interface GenericGameEventConsumer<T1, T2> : GameEventSubscriber<GenericGameEventInfo<T1, T2>> { }
}
