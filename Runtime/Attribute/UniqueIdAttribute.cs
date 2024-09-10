using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace Congroo.Core
{
    /// <summary>
    /// 唯一Id标签
    /// 使用此标签标记的类 会检测类内部的 const int 字段成员是否唯一
    /// 可以指定唯一Id的最小值 最大值区间
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class UniqueIdAttribute : Attribute
    {
        public uint Min;

        public uint Max;
        
        public UniqueIdAttribute(uint min = uint.MinValue, uint max = uint.MaxValue)
        {
            this.Min = min;
            this.Max = max;
        }
    }



    public static class UniqueIdValidCheck
    {
        public static void CheckAllType()
        {
            var assembly = Assembly.GetExecutingAssembly();
            foreach (var type in assembly.GetTypes())
            {
                Check(type);
            }
        }
        
        public static bool Check(Type type)
        {
            if (!type.IsDefined(typeof(UniqueIdAttribute), false))
            {
                return true;
            }

            UniqueIdAttribute uniqueIdAttribute = type.GetCustomAttributes(typeof(UniqueIdAttribute), false)[0] as UniqueIdAttribute;
            IEnumerable<FieldInfo> fields = type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance)
                .Where(f => f.IsLiteral && f.FieldType == typeof(uint));

            var values = new HashSet<uint>();
            foreach (var field in fields)
            {
                uint value = (uint)field.GetValue(null);
                if (value < uniqueIdAttribute.Min || value > uniqueIdAttribute.Max)
                {
                    Debug.LogError($"UniqueIdValidCheck {type} 存在非法值: {value}  应在这个范围内 {uniqueIdAttribute.Min}  {uniqueIdAttribute.Max}");
                    return false;
                }

                if (!values.Add(value))
                {
                    Debug.LogError($"UniqueIdValidCheck {type} 存在重复值: {value}");
                    return false;
                }
            }
            return true;
        }
    }
}