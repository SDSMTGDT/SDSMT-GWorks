using SDSMTGDT.GWorks.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDSMTGDT.GWorks.Story
{
    public class StoryNodePossibleEventInfo : GameEventInfo
    {
        public readonly StoryNode storyNode;
        internal StoryNodePossibleEventInfo(StoryNode storyNode)
        {
            this.storyNode = storyNode;
        }
    }
}
