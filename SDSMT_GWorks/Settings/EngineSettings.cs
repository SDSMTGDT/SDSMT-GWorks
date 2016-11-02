using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SDSMTGDT.GWorks.Settings
{
    //TODO: DECOUPLE FROM MANAGER
    public class EngineSettings
    {

        internal EngineSettings(SettingsManager manager)
        {
            WINDOW_HEIGHT = manager.AddSetting<int>();
            WINDOW_WIDTH = manager.AddSetting<int>();
            VSYNC = manager.AddSetting<bool>();
            VOLUME_MASTER = manager.AddSetting<int>();
            VOLUME_FX = manager.AddSetting<int>();
            VOLUME_MUSIC = manager.AddSetting<int>();
            VOLUME_STEREO = manager.AddSetting<bool>();
            DIRECTORY_CURRENT = manager.AddSetting<string>();
            DIRECTORY_SAVES = manager.AddSetting<string>();
        }

        public readonly SettingIndex<int> WINDOW_HEIGHT;
        public readonly SettingIndex<int> WINDOW_WIDTH;
        public readonly SettingIndex<bool> VSYNC;
        public readonly SettingIndex<int> VOLUME_MASTER;
        public readonly SettingIndex<int> VOLUME_FX;
        public readonly SettingIndex<int> VOLUME_MUSIC;
        public readonly SettingIndex<bool> VOLUME_STEREO;
        public readonly SettingIndex<string> DIRECTORY_CURRENT;
        public readonly SettingIndex<string> DIRECTORY_SAVES;
    }
}
