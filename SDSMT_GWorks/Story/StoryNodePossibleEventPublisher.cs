﻿using SDSMTGDT.Gworks.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDSMTGDT.Gworks.Story
{
    public class StoryNodePossibleEventPublisher
        : GameEventPublisher<StoryNodePossibleEventInfo>
    {
        internal StoryNodePossibleEventPublisher(EventManager eventManager)
            : base(eventManager, "Publishes storyboard events when story points become possible")
        { }

        internal void publish(StoryNode storyNode)
        {
            StoryNodePossibleEventInfo info = new StoryNodePossibleEventInfo(storyNode);
            fireEvent(info);
        }
    }
}