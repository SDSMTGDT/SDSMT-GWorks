using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SDSMTGDT.GWorks.Settings
{
    public class SettingsManager
    {
        private interface ISetting { }
        private class Setting<T> : ISetting
        {
            private T _value = default(T);
            public Type type { get; private set; } = typeof(T);
            public T value {
                get { return _value; }
                set { _value = value; settingUpdated?.Invoke(value); }
            } 
            public event Action<T> settingUpdated;
        }

        private List<ISetting> settings;

        public SettingsManager()
        {
            settings = new List<ISetting>();
            settings.Capacity = SettingIndexes.SETTINGS_SIZE;
            for (int i = 0; i < SettingIndexes.SETTINGS_SIZE; i++)
            {
                settings.Add(null);
            }
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
    }
}
