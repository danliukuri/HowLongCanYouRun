using UnityEngine;
using System;
using System.Collections.Generic;

namespace Utilities
{
    public static class JsonHelper
    {
        const char separator = ';';

        public static string ArrayToJson<T>(T[] array) where T : new()
        {
            string jsonArrayString = string.Empty;
            for (int i = 0; i < array.Length; i++)
                jsonArrayString += JsonUtility.ToJson(array[i]) + separator;
            return jsonArrayString;
        }

        public static T[] FromJsonToArray<T>(string jsonString) where T : new() => FromJsonToList<T>(jsonString).ToArray();
        public static List<T> FromJsonToList<T>(string jsonString) where T : new()
        {
            string[] jsonItemsStrings = jsonString.Split(new char[] { separator }, StringSplitOptions.RemoveEmptyEntries);
            List<T> items = new List<T>(jsonItemsStrings.Length);
            for (int i = 0; i < jsonItemsStrings.Length; i++)
            {
                items.Add(new T());
                JsonUtility.FromJsonOverwrite(jsonItemsStrings[i], items[i]);
            }
            return items;
        }
    }
}