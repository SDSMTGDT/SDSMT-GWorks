using SDSMTGDT.Gworks.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDSMTGDT.Gworks.Story
{
    public class StoryNodePassedEventInfo : GameEventInfo
    {
        public readonly StoryNode storyNode;
        internal StoryNodePassedEventInfo(StoryNode storyNode)
        {
            this.storyNode = storyNode;
        }
    }
}
