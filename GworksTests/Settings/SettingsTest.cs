using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace SDSMTGDT.GWorks.Settings
{
    /// <summary>
    /// This class contains the tests for the settings manager written in Nunit.
    /// </summary>
    [TestFixture]
    public class SettingsTest
    {
        private SettingsManager manager;
        private EngineSettings engineSettings;

        /// <summary>
        /// called to create a new manager before each test.
        /// </summary>
        [SetUp]
        public void setup()
        {
            manager = new SettingsManager();
            engineSettings = manager.EngineSettings;
        }

        /// <summary>
        /// This function Loads 100 into VOLUME_MASTER and checks it was done
        /// </summary>
        [Test]
        public void updateSettingFromNull()
        {
            manager.Update(engineSettings.VOLUME_MASTER, 100);
            Assert.AreEqual(manager.Access(engineSettings.VOLUME_MASTER), 100);
        }

        /// <summary>
        /// This function tests to see if the master volume has been 
        /// updated from the existing value
        /// </summary>
        [Test]
        public void updateSettingFromInitialized()
        {
            updateSettingFromNull();
            manager.Update(engineSettings.VOLUME_MASTER, 0);
            Assert.AreEqual(manager.Access(engineSettings.VOLUME_MASTER), 0);
        }

        /// <summary>
        /// Tests for settings updates
        /// When update is called, the corresponding lamdba should be
        /// executed
        /// </summary>
        [Test]
        public void testSettingEvents()
        {
            manager.AddUpdateListener(engineSettings.VOLUME_MASTER, (value) => { Assert.AreEqual(value, 50); });
            manager.Update(engineSettings.VOLUME_MASTER, 50);
        }
    }
}
