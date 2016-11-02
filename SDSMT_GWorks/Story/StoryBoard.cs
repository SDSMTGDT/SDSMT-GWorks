using System;
using System.Collections.Generic;
using SDSMTGDT.GWorks.Events;

namespace SDSMTGDT.GWorks.Story
{
    //TODO: Detect cycles in story
    [Serializable]
    public class StoryBoard 
    {
        private readonly Dictionary<uint, StoryNode> storyGraph;
        private uint nodeIdCounter;
        private readonly LinkedList<StoryNode> possibleNodes;
        private readonly StoryNodePassedEventPublisher passedPublisher;
        private readonly GameEventHook<StoryNodePassedEventInfo> passedHook;
        private readonly StoryNodePossibleEventPublisher possibilityPublisher;
        private readonly GameEventHook<StoryNodePossibleEventInfo> possibilityHook;
        public readonly string DESCRIPTION;

        public StoryBoard(EventManager manager, string description)
        {
            storyGraph = new Dictionary<uint, StoryNode>();
            possibleNodes = new LinkedList<StoryNode>();
            passedPublisher = new StoryNodePassedEventPublisher(manager);
            passedHook = new GameEventHook<StoryNodePassedEventInfo>(manager, passedPublisher.EVENT_ID);
            possibilityPublisher = new StoryNodePossibleEventPublisher(manager);
            possibilityHook = new GameEventHook<StoryNodePossibleEventInfo>(manager, possibilityPublisher.EVENT_ID);
            DESCRIPTION = description;
        }

        internal uint RegisterNewInnerNode(StoryNode storyNode)
        {
            uint temp = nodeIdCounter++;
            storyGraph.Add(temp, storyNode);
            return temp;
        }

        internal void UpdatePossibilities(StoryNode storyNode)
        {
            possibleNodes.Remove(storyNode);
            foreach(StoryNode successor in storyNode.GetSuccessors())
            {
                if (!possibleNodes.Contains(successor))
                {
                    bool possible = true;
                    foreach(StoryNode successorPredecessor in successor.GetPredecessors())
                    {
                        if (!successorPredecessor.GetState())
                            possible = false;
                    }
                    if (possible)
                        possibleNodes.AddLast(successor);
                }
            }
            passedPublisher.Publish(storyNode);
            possibilityPublisher.Publish(storyNode);
        }

        public void AddRootNode(StoryNode rootNode)
        {
            possibleNodes.AddFirst(rootNode);
        }

        public IEnumerable<StoryNode> GetPossibilities()
        {
            return possibleNodes;
        }

        public StoryNode GetStoryNodeById(uint id)
        {
            return storyGraph[id];
        }
        
        public GameEventHook<StoryNodePassedEventInfo> GetStoryNodePassedEventHook()
        {
            return passedHook;
        }

        public GameEventHook<StoryNodePossibleEventInfo> GetStoryNodePossibleEventHook()
        {
            return possibilityHook;
        }
    }
}
