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
            WINDOW_HEIGHT = manager.addSetting<uint>();
            WINDOW_WIDTH = manager.addSetting<uint>();
            VSYNC = manager.addSetting<bool>();
            VOLUME_MASTER = manager.addSetting<uint>();
            VOLUME_FX = manager.addSetting<uint>();
            VOLUME_MUSIC = manager.addSetting<uint>();
            VOLUME_STEREO = manager.addSetting<bool>();
            DIRECTORY_CURRENT = manager.addSetting<string>();
            DIRECTORY_SAVES = manager.addSetting<string>();
        }

        public readonly SettingIndex<uint> WINDOW_HEIGHT;
        public readonly SettingIndex<uint> WINDOW_WIDTH;
        public readonly SettingIndex<bool> VSYNC;
        public readonly SettingIndex<uint> VOLUME_MASTER;
        public readonly SettingIndex<uint> VOLUME_FX;
        public readonly SettingIndex<uint> VOLUME_MUSIC;
        public readonly SettingIndex<bool> VOLUME_STEREO;
        public readonly SettingIndex<string> DIRECTORY_CURRENT;
        public readonly SettingIndex<string> DIRECTORY_SAVES;
    }
}
