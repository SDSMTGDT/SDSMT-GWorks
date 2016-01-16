using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SDSMTGDT.GWorks.Settings
{
    public class SettingsManager
    {
        // ISetting allows for storing settings of different datatypes in the same list
        private interface ISetting { }

        // Setting stores a generic value, and fires an event when the value is updated
        private class Setting<T> : ISetting
        {
            private T _value = default(T);
            public T value {
                get { return _value; }
                set { _value = value; settingUpdated?.Invoke(value); }
            } 
            public event Action<T> settingUpdated;
        }

        // Contains the settings associated with the game
        private List<ISetting> settings;

        // Contains the indexes for the default game settings
        private EngineSettings engineSettings;

        public SettingsManager()
        {
            settings = new List<ISetting>();
            engineSettings = new EngineSettings(this);
        }

        public SettingIndex<T> addSetting<T>()
        {
            settings.Add(null);
            SettingIndex<T> newSetting = settings.Count - 1;
            return newSetting;
        }

        public void update<T>(SettingIndex<T> index, T value)
        {
            if (settings[index] == null)
            {
                settings[index] = new Setting<T>();
            }
            ((Setting<T>)settings[index]).value = value;
        }

        public T access<T>(SettingIndex<T> index)
        {
            if (settings[index] == null)
            {
                settings[index] = new Setting<T>();
            }
            return ((Setting<T>)settings[index]).value;
        }

        public void addUpdateListener<T>(SettingIndex<T> index, Action<T> updateListener)
        {
            if (settings[index] == null)
            {
                settings[index] = new Setting<T>();
            }
            ((Setting<T>)settings[index]).settingUpdated += updateListener;
        }        

        public EngineSettings getEngineSettings()
        {
            return engineSettings;
        }
    }
}
