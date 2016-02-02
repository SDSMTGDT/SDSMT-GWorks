using NUnit.Framework;
using SDSMTGDT.GWorks.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDSMTGDT.GWorks.Story
{
    [TestFixture]
    public class StoryBoardTest
    {
        private EventManager eventManager;
        private StoryBoard storyBoard;

        /// <summary>
        /// creates an eventManager and a storyBoard before each test.
        /// </summary>
        [SetUp]
        public void setUp()
        {
            eventManager = new EventManager();
            storyBoard = new StoryBoard(eventManager, "Main Storyboard");
        }

        /// <summary>
        /// Tests that an event that has been completed is appropriately
        /// handled.
        /// </summary>
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

        /// <summary>
        /// Tests that an event that can be completed is correctly marked as
        /// possible.
        /// </summary>
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
