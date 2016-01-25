using SDSMTGDT.Gworks.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDSMTGDT.Gworks.Story
{
    [Serializable]
    public class StoryNode
    {
        private StoryBoard storyBoard;
        private LinkedList<StoryNode> predecessors;
        private LinkedList<StoryNode> successors;
        private bool state;
        public readonly uint STORY_NODE_ID;
        public readonly string DESCRIPTION;

        public StoryNode(StoryBoard storyBoard,string description)
        {
            this.storyBoard = storyBoard;
            this.predecessors = new LinkedList<StoryNode>();
            this.successors = new LinkedList<StoryNode>();
            this.state = false;
            this.STORY_NODE_ID = storyBoard.registerNewInnerNode(this);
            this.DESCRIPTION = description;
        }

        public void setTrue()
        {
            foreach(StoryNode predecessor in predecessors)
            {
                if (predecessor.getState() == false)
                    throw new InvalidStoryStateException();
            }
            this.state = true;
            storyBoard.updatePossibilities(this);
        }
        
        public bool getState()
        {
            return state;
        }

        public IEnumerable<StoryNode> getPredecessors()
        {
            return predecessors;
        }

        public IEnumerable<StoryNode> getSuccessors()
        {
            return successors;
        }

        public void addSuccessor(StoryNode successor)
        {
            successors.AddLast(successor);
            successor.addPredecessor(this);
        }

        private void addPredecessor(StoryNode predecessor)
        {
            predecessors.AddLast(predecessor);
        }
    }
}
