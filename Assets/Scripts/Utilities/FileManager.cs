using System;
using System.IO;
using UnityEngine;

namespace Utilities
{
    public class FileManager
    {
        public static string FolderNameForSavingFiles { get; set; } = "SavedData";
        static string directoryForSavingFiles = Application.persistentDataPath + '/' + FolderNameForSavingFiles + '/';

        public static bool DoesTheFileExist(string filename) => File.Exists(directoryForSavingFiles + filename);
        public static void SaveStringToFile(string data, string filename)
        {
            if (string.IsNullOrWhiteSpace(filename))
                throw new ArgumentException($"'{nameof(filename)}' cannot be null or whitespace.", nameof(filename));

            if (!Directory.Exists(directoryForSavingFiles))
                Directory.CreateDirectory(directoryForSavingFiles);
            File.WriteAllText(directoryForSavingFiles + filename, data);
        }
        public static string LoadStringFromFile(string filename)
        {
            if (string.IsNullOrWhiteSpace(filename))
                throw new ArgumentException($"'{nameof(filename)}' cannot be null or whitespace.", nameof(filename));

            string fullPath = directoryForSavingFiles + filename;
            if (!File.Exists(fullPath))
                throw new ArgumentException();
            return File.ReadAllText(fullPath);
        }
       
    }
}