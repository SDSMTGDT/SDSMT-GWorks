using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace SDSMTGDT.Gworks.Events
{
    //TODO: write more tests
    [TestFixture]
    public class TestGenericEvents
    {
        EventManager manager;

        [SetUp]
        public void initEventManager()
        {
            manager = new EventManager();
        }

        [Test]
        public void testGenericOneItemEvent()
        {
            var stringProducer = new GenericGameEventProducer<string>(manager, "test string producer");
            string testValue = "test";
            stringProducer.registerEventListener((sender, info) => Assert.AreEqual(testValue, info.item1));
            stringProducer.publish(testValue);
            var intProducer = new GenericGameEventProducer<int>(manager, "test int producer");
            int testInteger = 23;
            intProducer.registerEventListener((sender, info) => Assert.AreEqual(testInteger, info.item1));
            intProducer.publish(testInteger);
        }
    }
}
