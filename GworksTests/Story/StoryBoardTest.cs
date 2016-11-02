using NUnit.Framework;
using SDSMTGDT.GWorks.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDSMTGDT.GWorks.Story
{
    /// <summary>
    /// The tests for our story board class written in Nunit.
    /// </summary>
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
            bool listenerRan = false;
            rootNode.AddSuccessor(new StoryNode(storyBoard, "secondA"));
            rootNode.AddSuccessor(new StoryNode(storyBoard, "secondB"));
            storyBoard.AddRootNode(rootNode);
            storyBoard.GetStoryNodePassedEventHook().RegisterEventListener(
                (sender, info) => {
                    Assert.True(info.StoryNode.DESCRIPTION == testDescription);
                    listenerRan = true;
                }
            );
            rootNode.SetTrue();
            Assert.True(listenerRan);
        }

        /// <summary>
        /// Tests that an event that can be completed is correctly marked as
        /// possible.
        /// </summary>
        [Test]
        public void testStoryPossibleEvent()
        {
            var rootNode = new StoryNode(storyBoard, "rootA");
            rootNode.AddSuccessor(new StoryNode(storyBoard, "secondA"));
            rootNode.AddSuccessor(new StoryNode(storyBoard, "secondB"));
            storyBoard.AddRootNode(rootNode);
            storyBoard.GetStoryNodePossibleEventHook().RegisterEventListener(
                (sender, info) => Assert.Pass()
            );
            rootNode.SetTrue();
            Assert.Fail();
        }
    }
}
