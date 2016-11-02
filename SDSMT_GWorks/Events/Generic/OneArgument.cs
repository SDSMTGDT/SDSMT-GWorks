namespace SDSMTGDT.GWorks.Events.Generic
{
    /// <summary>
    /// This class defines an event with one piece of information packaged.
    /// </summary>
    /// <typeparam name="T1">type of the data being packaged</typeparam>
    public class GenericGameEventInfo<T1> : GameEventInfo
    {
        /// <summary>
        /// Constructor for a one info event.
        /// </summary>
        /// <param name="item1"> the information that will sent</param>
        public GenericGameEventInfo(T1 item1)
        {
            Item1 = item1;
        }

        /// <summary>
        /// Container for the piece of information.
        /// </summary>
        public T1 Item1 { get; set; }
    }

    /// <summary>
    /// This class defines a game event publisher, this class will package our
    /// information to be sent with the event.
    /// </summary>
    /// <typeparam name="T1">type of the data being packaged</typeparam>
    public class GenericGameEventPublisher<T1> : GameEventPublisher<GenericGameEventInfo<T1>>
    {
        /// <summary>
        /// This constructor creates the generified game event publisher.
        /// </summary>
        /// <param name="manager">Event manager that will handle the event</param>
        /// <param name="description">description of the event</param>
        public GenericGameEventPublisher(EventManager manager, string description) 
            : base(manager, description)
        {

        }

        /// <summary>
        /// This method packages the event's info then fires the event.
        /// </summary>
        /// <param name="item1">the information that will be sent.</param>
        public void Publish(T1 item1)
        {
            GenericGameEventInfo<T1> info = new GenericGameEventInfo<T1>(item1);
            FireEvent(info);
        }

        /// <summary>
        /// This method publishes the event in a way such that it can be fired
        /// after a delay.
        /// </summary>
        /// <param name="item1">the information that will be sent</param>
        public void PublishDelayedEvent(T1 item1)
        {
            GenericGameEventInfo<T1> info = new GenericGameEventInfo<T1>(item1);
            QueueEvent(info);
        }
    }

    /// <summary>
    /// The interface for those that need information from the event.
    /// </summary>
    /// <typeparam name="T1">type of the data being packaged.</typeparam>
    public interface GenericGameEventConsumer<T1> : GameEventSubscriber<GenericGameEventInfo<T1>> { }
}
