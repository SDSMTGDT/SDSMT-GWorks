using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SDSMTGDT.GWorks.Settings
{
    public static class SettingIndexes
    {
        public static readonly SettingIndex<uint> WINDOW_HEIGHT = 0;
        public static readonly SettingIndex<uint> WINDOW_WIDTH = 1;
        public static readonly SettingIndex<bool> VSYNC = 2;
        public static readonly SettingIndex<uint> VOLUME_MASTER = 3;
        public static readonly SettingIndex<uint> VOLUME_FX = 4;
        public static readonly SettingIndex<uint> VOLUME_MUSIC = 5;
        public static readonly SettingIndex<bool> VOLUME_STEREO = 6;
        public static readonly SettingIndex<string> DIRECTORY_CURRENT = 7;
        public static readonly SettingIndex<string> DIRECTORY_SAVES = 8;
        
        public static int SETTINGS_SIZE = DIRECTORY_SAVES;
    }
}
