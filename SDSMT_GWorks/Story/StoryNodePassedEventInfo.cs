using SDSMTGDT.GWorks.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDSMTGDT.GWorks.Story
{
    public class StoryNodePassedEventInfo : GameEventInfo
    {
        public readonly StoryNode StoryNode;
        internal StoryNodePassedEventInfo(StoryNode storyNode)
        {
            StoryNode = storyNode;
        }
    }
}
