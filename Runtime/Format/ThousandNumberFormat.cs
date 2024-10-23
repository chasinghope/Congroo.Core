using System;

namespace Congroo.Core
{
    /// <summary>
    /// 输入必须是整数
    /// 如果大于一千，用逗号分隔 如：4567 表达为 4,567
    /// 如果大于一万，就用w标记 如 123456 表达为 12.3w
    /// </summary>
    public class ThousandNumberFormat : ICustomFormatter
    {
        public static ThousandNumberFormat Instance = new ();
        
        public string Format(string format, object arg, IFormatProvider formatProvider)
        {
            if (arg == null)
            {
                return String.Empty;
            }

            // 确保输入是整数
            if (arg is int number)
            {
                // 大于1000，格式化为带逗号的字符串
                if (number > 1000)
                {
                    // 大于10000，格式化为带 "w" 的字符串
                    if (number > 10000)
                    {
                        // 计算万的部分
                        return (number / 10000.0).ToString("F1") + "w";
                    }
                    else
                    {
                        // 使用逗号分隔
                        return number.ToString("N0");
                    }
                }
                else
                {
                    // 直接返回数字的字符串形式
                    return number.ToString();
                }
            }
            else
            {
                throw new ArgumentException("输入必须是整数");
            }
                
        }
    }
}
