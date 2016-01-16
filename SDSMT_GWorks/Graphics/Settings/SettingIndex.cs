using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SDSMTGDT.DungeonCrawler.Settings
{
    //Class that maps types to integers
    //The type is passed in by generics
    //The index is passed in by the constructor and accessible via properties
    //The last method allows access of the index via casting
    public struct SettingIndex<T>
    {
        private int index { get; }

        public SettingIndex(int index)
        {
            this.index = index;
        }

        public static implicit operator int (SettingIndex<T> instance)
        {
            return instance.index;
        }

        public static implicit operator SettingIndex<T>(int v)
        {
            return new SettingIndex<T>(v);
        }
    }
}
