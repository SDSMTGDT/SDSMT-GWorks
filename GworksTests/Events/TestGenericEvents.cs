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
            var stringPublisher = new GenericGameEventPublisher<string>();
            string testValue = "test";
            stringPublisher.addListener((sender, info) => Assert.AreEqual(testValue, info.item1));
            stringPublisher.publish(testValue);
            var intPublisher = new GenericGameEventPublisher<int>();
            int testInteger = 23;
            intPublisher.addListener((sender, info) => Assert.AreEqual(testInteger, info.item1));
            intPublisher.publish(testInteger);
        }
    }
}
