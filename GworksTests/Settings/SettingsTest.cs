using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using static SDSMTGDT.GWorks.Settings.SettingIndexes;

namespace SDSMTGDT.GWorks.Settings
{
    [TestFixture]
    public class SettingsTest
    {
        private SettingsManager manager;
        //called to create a new manager before each test.
        [SetUp]
        public void setup()
        {
            manager = new SettingsManager();
        }

        //This function Loads 100 into VOLUME_MASTER and checks it was done
        [Test]
        public void updateFromNull()
        {
            manager.update<uint>(VOLUME_MASTER, 100);
            Assert.AreEqual(manager.access(VOLUME_MASTER), 100);
        }

        //This function tests to see if the master volume has been 
        //updated from the existing value
        [Test]
        public void updateFromInitialized()
        {
            updateFromNull();
            manager.update<uint>(VOLUME_MASTER, 0);
            Assert.AreEqual(manager.access(VOLUME_MASTER), 0);
        }

        //Tests for settings updates
        //When update is called, the corresponding lamdba should be
        //executed
        [Test]
        public void testEvents()
        {
            manager.addUpdateListener(VOLUME_MASTER, (value) => { Assert.AreEqual(value, 50); });
            manager.update<uint>(VOLUME_MASTER, 50);
        }
    }
}
