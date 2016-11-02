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
    [TestFixture]
    class TestDisposeEvents
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
            gameStateManager = new GameStateManager(null);
            eventManager = gameStateManager.Events;
        }

        [Test]
        public void testDispose()
        {
            //test value
            int testValue = 100;
            //event counts
            int eventCountFirst = 0;
            int eventCountSecond = 0;
            //create our publisher
            GenericGameEventPublisher<int> myPublisher = 
                new GenericGameEventPublisher<int>
                (gameStateManager.Events, "Test publisher");
            //register the listener
            myPublisher.RegisterEventListener((sender, info) =>
            {
                Assert.AreEqual(testValue, info.Item1);
            });
            //have the publisher publish something
            myPublisher.Publish(100);
            //check the number of events in the event map
            eventCountFirst = eventManager.GetEventCount();
            //dispose of the publisher
            myPublisher.Dispose();
            //check the number of events in the event map after disposal
            eventCountSecond = eventManager.GetEventCount();
            //assert that the value has changed.
            Assert.AreEqual(eventCountFirst, 1);
            Assert.AreEqual(eventCountSecond, 0);
        }
    }
}
