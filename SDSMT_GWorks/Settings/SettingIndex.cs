using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SDSMTGDT.GWorks.Settings
{
    //Class that maps types to integers
    //The type is passed in by generics
    //The index is passed in by the constructor and accessible via properties
    //The last method allows access of the index via casting
    public struct SettingIndex<T>
    {
        private int Index { get; }

        public SettingIndex(int index)
        {
            Index = index;
        }

        public static implicit operator int (SettingIndex<T> instance)
        {
            return instance.Index;
        }

        public static implicit operator SettingIndex<T>(int v)
        {
            return new SettingIndex<T>(v);
        }
    }
}
