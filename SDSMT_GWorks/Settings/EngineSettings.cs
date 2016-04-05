using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SDSMTGDT.GWorks.Settings
{
    public class EngineSettings
    {
        private SettingsManager manager;

        internal EngineSettings(SettingsManager manager)
        {
            this.manager = manager;
            WINDOW_HEIGHT = manager.addSetting<int>();
            WINDOW_WIDTH = manager.addSetting<int>();
            VSYNC = manager.addSetting<bool>();
            VOLUME_MASTER = manager.addSetting<int>();
            VOLUME_FX = manager.addSetting<int>();
            VOLUME_MUSIC = manager.addSetting<int>();
            VOLUME_STEREO = manager.addSetting<bool>();
            DIRECTORY_CURRENT = manager.addSetting<string>();
            DIRECTORY_SAVES = manager.addSetting<string>();
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
