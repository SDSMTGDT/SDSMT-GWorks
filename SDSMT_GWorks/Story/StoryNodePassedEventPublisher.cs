using SDSMTGDT.Gworks.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDSMTGDT.Gworks.Story
{
    public class StoryNodePassedEventPublisher
        : GameEventPublisher<StoryNodePassedEventInfo>
    {
        internal StoryNodePassedEventPublisher(EventManager eventManager)
            : base(eventManager, "Publishes storyboard events when story points have been passed.")
        { }

        internal void publish(StoryNode storyNode)
        {
            StoryNodePassedEventInfo info = new StoryNodePassedEventInfo(storyNode);
            fireEvent(info);
        }
    }
}
