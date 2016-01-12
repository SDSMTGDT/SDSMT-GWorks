using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using static SDSMTGDT.DungeonCrawler.Settings.SettingIndexes;

namespace SDSMTGDT.DungeonCrawler.Settings
{
    [TestFixture]
    public class SettingsTest
    {
        private SettingsManager manager;
        [SetUp]
        public void setup()
        {
            manager = new SettingsManager();
        }

        [Test]
        public void updateFromNull()
        {
            manager.update<uint>(VOLUME_MASTER, 100);
            Assert.AreEqual(manager.access(VOLUME_MASTER), 100);
        }

        [Test]
        public void updateFromInitialized()
        {
            updateFromNull();
            manager.update<uint>(VOLUME_MASTER, 0);
            Assert.AreEqual(manager.access(VOLUME_MASTER), 0);
        }

        [Test]
        public void testEvents()
        {
            manager.addUpdateListener(VOLUME_MASTER, (value) => { Assert.AreEqual(value, 50); });
            manager.update<uint>(VOLUME_MASTER, 50);
        }
    }
}
