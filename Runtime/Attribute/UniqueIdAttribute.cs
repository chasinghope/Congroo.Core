using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace Congroo.Core
{
    /// <summary>
    /// ΨһId��ǩ
    /// ʹ�ô˱�ǩ��ǵ��� �������ڲ��� const int �ֶγ�Ա�Ƿ�Ψһ
    /// ����ָ��ΨһId����Сֵ ���ֵ����
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
                    Debug.LogError($"UniqueIdValidCheck {type} ���ڷǷ�ֵ: {value}  Ӧ�������Χ�� {uniqueIdAttribute.Min}  {uniqueIdAttribute.Max}");
                    return false;
                }

                if (!values.Add(value))
                {
                    Debug.LogError($"UniqueIdValidCheck {type} �����ظ�ֵ: {value}");
                    return false;
                }
            }
            return true;
        }
    }
}