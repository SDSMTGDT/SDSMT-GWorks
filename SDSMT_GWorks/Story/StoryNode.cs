using System;
using System.Collections.Generic;

namespace SDSMTGDT.GWorks.Story
{
    [Serializable]
    public class StoryNode
    {
        private readonly StoryBoard storyBoard;
        private readonly LinkedList<StoryNode> predecessors;
        private readonly LinkedList<StoryNode> successors;
        private bool state;
        public readonly uint STORY_NODE_ID;
        public readonly string DESCRIPTION;

        public StoryNode(StoryBoard storyBoard,string description)
        {
            this.storyBoard = storyBoard;
            predecessors = new LinkedList<StoryNode>();
            successors = new LinkedList<StoryNode>();
            state = false;
            STORY_NODE_ID = storyBoard.RegisterNewInnerNode(this);
            DESCRIPTION = description;
        }

        public void SetTrue()
        {
            foreach(StoryNode predecessor in predecessors)
            {
                if (predecessor.GetState() == false)
                    throw new InvalidStoryStateException();
            }
            state = true;
            storyBoard.UpdatePossibilities(this);
        }
        
        public bool GetState()
        {
            return state;
        }

        public IEnumerable<StoryNode> GetPredecessors()
        {
            return predecessors;
        }

        public IEnumerable<StoryNode> GetSuccessors()
        {
            return successors;
        }

        public void AddSuccessor(StoryNode successor)
        {
            successors.AddLast(successor);
            successor.AddPredecessor(this);
        }

        private void AddPredecessor(StoryNode predecessor)
        {
            predecessors.AddLast(predecessor);
        }
    }
}
