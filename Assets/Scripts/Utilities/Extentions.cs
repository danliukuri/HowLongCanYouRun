using System;
using UnityEngine;

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

        /// <summary>
        /// Destroys all children of gameobject transform
        /// </summary>
        public static void DestroyChilds(this Transform transform)
        {
            for (int i = transform.childCount - 1; i >= 0; i--)
                GameObject.Destroy(transform.GetChild(i).gameObject);
        }
    }
}