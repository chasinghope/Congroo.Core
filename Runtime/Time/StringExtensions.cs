using System;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;

namespace Congroo.Core
{
    public static class StringExtensions
    {
        /// <summary>
        /// string转枚举
        /// </summary>
        /// <param name="str"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T ToEnum<T>(this string str)
        {
            try
            {
                return (T)Enum.Parse(typeof(T), str);
            }
            catch (Exception e)
            {
                Debug.Log(e);
                return default;
            }
        }

        /// <summary>
        /// 移除所有非汉字、字母、数字的字符
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string RemoveTitleSymbol(this string str)
        {
            return Regex.Replace(str, @"[^\u4e00-\u9fa5a-zA-Z0-9]", "");
        }

        /// <summary>
        /// 移除空格
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string RemoveSpace(this string str)
        {
            return Regex.Replace(str, @"\s", "");
        }
        
        /// <summary>
        /// 判断文件是否存在
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static bool IsFileExist(this string path)
        {
            // 判断文件路径是否为空或空白字符串
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentException("File path is null or empty.");
            }
            return System.IO.File.Exists(path);
        }
        
        /// <summary>
        /// URL中提取文件名
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string ExtractFileNameFromUrl(this string url)
        {
            if (string.IsNullOrEmpty(url))
                return string.Empty;

            if (Uri.TryCreate(url, UriKind.Absolute, out var uri))
            {
                // 提取路径部分
                string path = uri.AbsolutePath;
                // 获取最后一个斜杠后的字符串，即文件名
                int lastSlashIndex = path.LastIndexOf('/');
                if (lastSlashIndex >= 0 && lastSlashIndex < path.Length - 1)
                {
                    return path.Substring(lastSlashIndex + 1);
                }
            }

            return string.Empty;
        }
        
        /// <summary>
        ///  将字符串转换为Base64编码
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ToBase64(this string str)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(str);
            return Convert.ToBase64String(bytes);
        }
        
        /// <summary>
        ///  检查字符串是否包含特定子字符串,忽略大小写
        /// </summary>
        /// <param name="str"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool ContainsIgnoreCase(this string str, string value)
        {
            return str.IndexOf(value, StringComparison.OrdinalIgnoreCase) >= 0;
        }
        
        /// <summary>
        /// 反转字符串
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string Reverse(this string str)
        {
            char[] charArray = str.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }
        
        /// <summary>
        ///  判断字符串是否为邮箱地址格式
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsEmail(this string str)
        {
            return Regex.IsMatch(str, @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$");
        }
        
    }
}