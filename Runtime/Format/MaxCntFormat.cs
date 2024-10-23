using System;

namespace Congroo.Core
{
    public class MaxCntFormat : IFormatProvider, ICustomFormatter
    {
        public static MaxCntFormat instance;
        public static MaxCntFormat Instance
        {
            get
            {
                if (instance == null)
                    instance = new MaxCntFormat();
                return instance;
            }
        }

        public object GetFormat(Type formatType)
        {
            if (formatType == typeof(ICustomFormatter))
            {
                return this;
            }
            return null;
        }

        public string Format(string format, object arg, IFormatProvider formatProvider)
        {
            if (arg == null || format != null || !(arg is string))
            {
                return null;
            }

            string input = arg.ToString();
            if (input.Length > 5)
            {
                input = input.Substring(0, 5) + "...";
            }

            return input;
        }

        public static string Filter(object arg)
        {
            return string.Format(MaxCntFormat.Instance, "{0}", arg);
        }

    }



    public class TenthousandFormat : IFormatProvider, ICustomFormatter
    {
        public static TenthousandFormat instance;
        public static TenthousandFormat Instance
        {
            get
            {
                if (instance == null)
                    instance = new TenthousandFormat();
                return instance;
            }
        }

        public object GetFormat(Type formatType)
        {
            if (formatType == typeof(ICustomFormatter))
            {
                return this;
            }
            return null;
        }

        public string Format(string format, object arg, IFormatProvider formatProvider)
        {
            if (arg == null || format != null)
            {
                return null;
            }

            if (!float.TryParse(arg.ToString(), out float value))
            {
                return "error...";
            }

            string output = value.ToString();
            if (value >= 10000)
            {
                output = $"{Math.Round(value / 10000, 2)}w";
            }
            
            return output;
        }

        public static string Filter(object arg)
        {
            return string.Format(Instance, "{0}", arg);
        }

    }
}