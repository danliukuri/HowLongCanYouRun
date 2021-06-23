using UnityEngine;
using System;

namespace Utilities
{
    public static class JsonHelper
    {
        public static string ToJson<T>(T[] array)
        {
            if (array is null)
                throw new ArgumentNullException(nameof(array));
            return JsonUtility.ToJson(new SerializableArrayWrapper<T>() { Array = array });
        }
        public static T[] FromJson<T>(string json)
        {
            if (string.IsNullOrWhiteSpace(json))
                throw new ArgumentException($"'{nameof(json)}' cannot be null or whitespace.", nameof(json));

            return JsonUtility.FromJson<SerializableArrayWrapper<T>>(json).Array;
        }
        
        [Serializable]
        class SerializableArrayWrapper<T>
        {
            public T[] Array { get => array; set => array = value; }
            [SerializeField] T[] array;
        }
    }
}