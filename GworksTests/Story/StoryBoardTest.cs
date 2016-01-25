using NUnit.Framework;
using SDSMTGDT.Gworks.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDSMTGDT.Gworks.Story
{
    [TestFixture]
    public class StoryBoardTest
    {
        private EventManager eventManager;
        private StoryBoard storyBoard;
        [SetUp]
        public void setUp()
        {
            eventManager = new EventManager();
            storyBoard = new StoryBoard(eventManager, "Main Storyboard");
        }

        [Test]
        public void testStoryPassedEvent()
        {
            var testDescription = "rootA";
            var rootNode = new StoryNode(storyBoard, testDescription);
            rootNode.addSuccessor(new StoryNode(storyBoard, "secondA"));
            rootNode.addSuccessor(new StoryNode(storyBoard, "secondB"));
            storyBoard.addRootNode(rootNode);
            eventManager.registerEventListener(
                storyBoard.getStoryNodePassedEventType(),
                (sender, info) => Assert.True(info.storyNode.DESCRIPTION == testDescription)
            );
            rootNode.setTrue();
        }

        [Test]
        public void testStoryPossibleEvent()
        {
            var rootNode = new StoryNode(storyBoard, "rootA");
            rootNode.addSuccessor(new StoryNode(storyBoard, "secondA"));
            rootNode.addSuccessor(new StoryNode(storyBoard, "secondB"));
            storyBoard.addRootNode(rootNode);
            eventManager.registerEventListener(
                storyBoard.getStoryNodePossibleEventType(),
                (sender, info) => Assert.Pass()
            );
            rootNode.setTrue();
            Assert.Fail();
        }
    }
}
