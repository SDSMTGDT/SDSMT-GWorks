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
        [Test]
        public void testGenericOneItemEvent()
        {
            var stringProducer = new GenericGameEventProducer<string>();
            string testValue = "test";
            stringProducer.addListener((sender, info) => Assert.AreEqual(testValue, info.item1));
            stringProducer.publish(testValue);
            var intProducer = new GenericGameEventProducer<int>();
            int testInteger = 23;
            intProducer.addListener((sender, info) => Assert.AreEqual(testInteger, info.item1));
            intProducer.publish(testInteger);
        }
    }
}
