using System;

namespace Extensions
{
    public static class StringParseOrDefault
    {
        /// <summary>
        /// If the string is empty or null returns default value of the <code>T</code>.
        /// If string is valid number returns in the T type.
        /// </summary>
        /// <param name="argumentString"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns>Default of T or number in string.</returns>
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