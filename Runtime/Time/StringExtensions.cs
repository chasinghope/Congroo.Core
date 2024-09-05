using System;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;

namespace Congroo.Core
{
    public static class StringExtensions
    {
        /// <summary>
        /// stringתö��
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
        /// �Ƴ����зǺ��֡���ĸ�����ֵ��ַ�
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string RemoveTitleSymbol(this string str)
        {
            return Regex.Replace(str, @"[^\u4e00-\u9fa5a-zA-Z0-9]", "");
        }

        /// <summary>
        /// �Ƴ��ո�
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string RemoveSpace(this string str)
        {
            return Regex.Replace(str, @"\s", "");
        }
        
        /// <summary>
        /// �ж��ļ��Ƿ����
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static bool IsFileExist(this string path)
        {
            // �ж��ļ�·���Ƿ�Ϊ�ջ�հ��ַ���
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentException("File path is null or empty.");
            }
            return System.IO.File.Exists(path);
        }
        
        /// <summary>
        /// URL����ȡ�ļ���
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string ExtractFileNameFromUrl(this string url)
        {
            if (string.IsNullOrEmpty(url))
                return string.Empty;

            if (Uri.TryCreate(url, UriKind.Absolute, out var uri))
            {
                // ��ȡ·������
                string path = uri.AbsolutePath;
                // ��ȡ���һ��б�ܺ���ַ��������ļ���
                int lastSlashIndex = path.LastIndexOf('/');
                if (lastSlashIndex >= 0 && lastSlashIndex < path.Length - 1)
                {
                    return path.Substring(lastSlashIndex + 1);
                }
            }

            return string.Empty;
        }
        
        /// <summary>
        ///  ���ַ���ת��ΪBase64����
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ToBase64(this string str)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(str);
            return Convert.ToBase64String(bytes);
        }
        
        /// <summary>
        ///  ����ַ����Ƿ�����ض����ַ���,���Դ�Сд
        /// </summary>
        /// <param name="str"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool ContainsIgnoreCase(this string str, string value)
        {
            return str.IndexOf(value, StringComparison.OrdinalIgnoreCase) >= 0;
        }
        
        /// <summary>
        /// ��ת�ַ���
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
        ///  �ж��ַ����Ƿ�Ϊ�����ַ��ʽ
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsEmail(this string str)
        {
            return Regex.IsMatch(str, @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$");
        }
        
    }
}