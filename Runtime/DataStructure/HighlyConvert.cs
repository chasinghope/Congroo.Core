using System;
using System.Collections.Generic;
using System.Reflection;

namespace Congroo.Core
{
    public static class HighlyConvert
    {
        static FieldInfo int16_itemFiled;
        static FieldInfo int32_itemFiled;
        static FieldInfo int64_itemFiled;
        static FieldInfo uint16_itemFiled;
        static FieldInfo uint32_itemFiled;
        static FieldInfo uint64_itemFiled;
        static FieldInfo float_itemFiled;
        static FieldInfo double_itemFiled;
        static FieldInfo string_itemFiled;
        
        public static Int16[] GetArray(List<Int16> list)
        {
            if(int16_itemFiled == null)
                int16_itemFiled = typeof(List<Int16>).GetField("_items", BindingFlags.NonPublic | BindingFlags.Instance);
            return int16_itemFiled.GetValue(list) as Int16[];
        }
        public static Int32[] GetArray(List<Int32> list)
        {
            if (int32_itemFiled == null)
                int32_itemFiled = typeof(List<Int32>).GetField("_items", BindingFlags.NonPublic | BindingFlags.Instance);
            return int32_itemFiled.GetValue(list) as Int32[];
        }

        public static Int64[] GetArray(List<Int64> list)
        {
            if (int64_itemFiled == null)
                int64_itemFiled = typeof(List<Int64>).GetField("_items", BindingFlags.NonPublic | BindingFlags.Instance);
            return int64_itemFiled.GetValue(list) as Int64[];
        }

        public static UInt16[] GetArray(List<UInt16> list)
        {
            if (uint16_itemFiled == null)
                uint16_itemFiled = typeof(List<UInt16>).GetField("_items", BindingFlags.NonPublic | BindingFlags.Instance);
            return uint16_itemFiled.GetValue(list) as UInt16[];
        }
        public static UInt32[] GetArray(List<UInt32> list)
        {
            if (uint32_itemFiled == null)
                uint32_itemFiled = typeof(List<UInt32>).GetField("_items", BindingFlags.NonPublic | BindingFlags.Instance);
            return uint32_itemFiled.GetValue(list) as UInt32[];
        }

        public static UInt64[] GetArray(List<UInt64> list)
        {
            if(uint64_itemFiled == null)
                uint64_itemFiled = typeof(List<UInt64>).GetField("_items", BindingFlags.NonPublic | BindingFlags.Instance);
            return uint64_itemFiled.GetValue(list) as UInt64[];
        }

        public static float[] GetArray(List<float> list)
        {
            if(float_itemFiled == null)
                float_itemFiled = typeof(List<float>).GetField("_items", BindingFlags.NonPublic | BindingFlags.Instance);
            return float_itemFiled.GetValue(list) as float[];
        }

        public static double[] GetArray(List<double> list)
        {
            if (double_itemFiled == null)
                double_itemFiled = typeof(List<double>).GetField("_items", BindingFlags.NonPublic | BindingFlags.Instance);
            return double_itemFiled.GetValue(list) as double[];
        }

        public static string[] GetArray(List<string> list)
        {
            if(string_itemFiled == null)
                string_itemFiled = typeof(List<string>).GetField("_items", BindingFlags.NonPublic | BindingFlags.Instance);
            return string_itemFiled.GetValue(list) as string[];
        }

    }
}