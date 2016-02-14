using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using SDSMTGDT.GWorks.GameStates;
using SDSMTGDT.GWorks.Events.Generic;

namespace SDSMTGDT.GWorks.Events
{
    //TODO: write more tests
    /// <summary>
    /// This class contains the tests we have written with Nunit.
    /// </summary>
    [TestFixture]
    public class TestGenericEvents
    {
        EventManager eventManager;
        GameStateManager gameStateManager;

        /// <summary>
        /// Creates a new gameStateManager and gives it a new eventManager.
        /// This is done before each test.
        /// </summary>
        [SetUp]
        public void initEventManager()
        {
            gameStateManager = new GameStateManager();
            eventManager = gameStateManager.getEventManager();
        }

        /// <summary>
        /// Registers an event with one piece of information and then tests
        /// that a listener was updated correctly with this information.
        /// </summary>
        [Test]
        public void testGenericOneItemEvent()
        {
            bool firstPassed = false, secondPassed = false;
            var stringPublisher = new GenericGameEventPublisher<string>(eventManager, "test string publisher");
            string testValue = "test";
            stringPublisher.registerEventListener((sender, info) =>
                {
                    Assert.AreEqual(testValue, info.item1);
                    firstPassed = true;
                });
            stringPublisher.publish(testValue);
            var intPublisher = new GenericGameEventPublisher<int>(eventManager, "test int publisher");
            int testInteger = 23;
            intPublisher.registerEventListener((sender, info) => 
            {
                Assert.AreEqual(testInteger, info.item1);
                secondPassed = true;
            });
            intPublisher.publish(testInteger);
            if (!firstPassed || !secondPassed)
                Assert.Fail();
        }

        /// <summary>
        /// Registers an event with two pieces of information and tests that
        /// a listener was updated correctly with this information.
        /// </summary>
        [Test]
        public void testGenericTwoItemEvent()
        {
            bool firstPassed = false, secondPassed = false;
            var stringPublisher = new GenericGameEventPublisher<string, string>(eventManager, "test string publisher");
            string testValue1 = "test";
            string testValue2 = "test2";
            stringPublisher.registerEventListener((sender, info) =>
            {
                Assert.AreEqual(testValue1, info.item1);
                Assert.AreEqual(testValue2, info.item2);
                firstPassed = true;
            });
            stringPublisher.publish(testValue1,testValue2);
            var intPublisher = new GenericGameEventPublisher<int, int>(eventManager, "test int publisher");
            int testInteger1 = 23;
            int testInteger2 = 24;
            intPublisher.registerEventListener((sender, info) =>
            {
                Assert.AreEqual(testInteger1, info.item1);
                Assert.AreEqual(testInteger2, info.item2);
                secondPassed = true;
            });
            intPublisher.publish(testInteger1, testInteger2);
            if (!firstPassed || !secondPassed)
                Assert.Fail();
        }

        /// <summary>
        /// Registers an event that does not activate immediately. This event
        /// is then fired and a listener is tested to ensure it updated
        /// correctly.
        /// </summary>
        [Test]
        public void testGenericOneItemDelayedEvent()
        {
            bool passedFirst = false, passedSecond = false;
            var stringPublisher = new GenericGameEventPublisher<string>(eventManager, "test string publisher");
            string testValue = "test";
            stringPublisher.registerEventListener(
                (sender, info) => {
                    Assert.AreEqual(testValue, info.item1);
                    passedFirst = true;
                }
            );
            stringPublisher.publishDelayedEvent(testValue);
            var intPublisher = new GenericGameEventPublisher<int>(eventManager, "test int publisher");
            int testInteger = 23;
            intPublisher.registerEventListener(
                (sender, info) => {
                    Assert.AreEqual(testInteger, info.item1);
                    passedSecond = true;
                }
            );
            intPublisher.publishDelayedEvent(testInteger);
            gameStateManager.update(new Microsoft.Xna.Framework.GameTime());
            if (!passedFirst || !passedSecond)
                Assert.Fail();
        }

        /// <summary>
        /// registers an event and then tests to ensure that both of the
        /// listeners were updated correctly.
        /// </summary>
        [Test]
        public void testMultipleEventListeners()
        {
            bool firstPassed = false, secondPassed = false;
            var stringPublisher = new GenericGameEventPublisher<string>(eventManager, "test string publisher");
            string testValue = "test";
            stringPublisher.registerEventListener(
                (sender, info) => 
                {
                    Assert.AreEqual(testValue, info.item1);
                    firstPassed = true;
                });
            stringPublisher.registerEventListener(
                (sender, info) =>
                {
                    Assert.AreEqual(testValue, info.item1);
                    secondPassed = true;
                });
            stringPublisher.publish(testValue);
            if (!firstPassed || !secondPassed)
                Assert.Fail();
        }

        /// <summary>
        /// Registers an event but does not fire it immediately. Then the event
        /// is fired and multiple listeners are tested to ensure that they
        /// update correctly.
        /// </summary>
        [Test]
        public void testMultipleDelayedEventListeners()
        {
            bool firstPassed = false, secondPassed = false;
            var stringPublisher = new GenericGameEventPublisher<string>(eventManager, "test string publisher");
            string testValue = "test";
            stringPublisher.registerEventListener(
                (sender, info) =>
                {
                    Assert.AreEqual(testValue, info.item1);
                    firstPassed = true;
                });
            stringPublisher.registerEventListener(
                (sender, info) =>
                {
                    Assert.AreEqual(testValue, info.item1);
                    secondPassed = true;
                });
            stringPublisher.publishDelayedEvent(testValue);
            gameStateManager.update(new Microsoft.Xna.Framework.GameTime());
            if (!firstPassed || !secondPassed)
                Assert.Fail();
        }
    }
}
