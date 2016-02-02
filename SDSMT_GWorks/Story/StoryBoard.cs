using SDSMTGDT.Gworks.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDSMTGDT.Gworks.Story
{
    //TODO: Detect cycles in story
    [Serializable]
    public class StoryBoard 
    {
        private Dictionary<uint, StoryNode> storyGraph;
        private uint nodeIdCounter = 0;
        private LinkedList<StoryNode> possibleNodes;
        private StoryNodePassedEventPublisher passedPublisher;
        private StoryNodePossibleEventPublisher possibilityPublisher;
        public readonly string DESCRIPTION;

        public StoryBoard(EventManager manager, string description)
        {
            this.storyGraph = new Dictionary<uint, StoryNode>();
            this.possibleNodes = new LinkedList<StoryNode>();
            this.passedPublisher = new StoryNodePassedEventPublisher(manager);
            this.possibilityPublisher = new StoryNodePossibleEventPublisher(manager);
            this.DESCRIPTION = description;
        }

        internal uint registerNewInnerNode(StoryNode storyNode)
        {
            uint temp = nodeIdCounter++;
            storyGraph.Add(temp, storyNode);
            return temp;
        }

        internal void updatePossibilities(StoryNode storyNode)
        {
            possibleNodes.Remove(storyNode);
            foreach(StoryNode successor in storyNode.getSuccessors())
            {
                if (!possibleNodes.Contains(successor))
                {
                    bool possible = true;
                    foreach(StoryNode successorPredecessor in successor.getPredecessors())
                    {
                        if (!successorPredecessor.getState())
                            possible = false;
                    }
                    if (possible)
                        possibleNodes.AddLast(successor);
                }
            }
            passedPublisher.publish(storyNode);
            possibilityPublisher.publish(storyNode);
        }

        public void addRootNode(StoryNode rootNode)
        {
            possibleNodes.AddFirst(rootNode);
        }

        public IEnumerable<StoryNode> getPossibilities()
        {
            return possibleNodes;
        }

        public StoryNode getStoryNodeById(uint id)
        {
            return storyGraph[id];
        }

        public EventType<StoryNodePassedEventInfo> getStoryNodePassedEventType()
        {
            return passedPublisher.EVENT_TYPE;
        }

        public EventType<StoryNodePossibleEventInfo> getStoryNodePossibleEventType()
        {
            return possibilityPublisher.EVENT_TYPE;
        }
    }
}
