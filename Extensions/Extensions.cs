using System;

namespace Extensions
{
    public static class StringParseOrDefault
    {
        public static T ParseOrDefault<T>(this string argumentString)
        {
            if (String.IsNullOrEmpty(argumentString))
            {
                return default(T);
            }
            else
            {
                return (T)Convert.ChangeType(argumentString, typeof(T));
            }

        }
    }
}