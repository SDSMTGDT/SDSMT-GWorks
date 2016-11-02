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
            private T value = default(T);
            public T Value {
                get { return value; }
                set { this.value = value; SettingUpdated?.Invoke(value); }
            } 
            public event Action<T> SettingUpdated;
        }

        // Contains the settings associated with the game
        private readonly List<ISetting> settings;

        // Contains the indexes for the default game settings
        public EngineSettings EngineSettings { get; private set; }

        public SettingsManager()
        {
            settings = new List<ISetting>();
            EngineSettings = new EngineSettings(this);
        }

        public SettingIndex<T> AddSetting<T>()
        {
            settings.Add(new Setting<T>());
            SettingIndex<T> newSetting = settings.Count - 1;
            return newSetting;
        }

        public void Update<T>(SettingIndex<T> index, T value)
        {
            ((Setting<T>)settings[index]).Value = value;
        }

        public T Access<T>(SettingIndex<T> index)
        {
            return ((Setting<T>)settings[index]).Value;
        }

        public void AddUpdateListener<T>(SettingIndex<T> index, Action<T> updateListener)
        {
            ((Setting<T>)settings[index]).SettingUpdated += updateListener;
        }        

        public bool RemoveUpdateListener<T>(SettingIndex<T> index, Action<T> updateListener)
        {
            ((Setting<T>)settings[index]).SettingUpdated -= updateListener;
            return true;
        }
    }
}
