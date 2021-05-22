using System;

namespace Utilities
{
    public static class Extentions
    {
        /// <summary>
        /// Calls action in a "for" loop for all elements
        /// </summary>
        /// <param name="action">сalled for array elements</param>
        public static void ForAll<T>(this T[] array, Action<T> action)
        {
            for (int i = 0; i < array.Length; i++)
                action(array[i]);
        }
    }
}