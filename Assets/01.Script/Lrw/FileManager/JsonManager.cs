using System.IO;
using UnityEngine;

namespace _01.Script.Lrw.FileManager
{
    public static class JsonManager
    {
        private static readonly string FolderName = "GameDataFolder";
        private static readonly string BaseFileName = "GameDataFile";

        private static string _saveFolderPath;
        
        public static void CreatFolder()
        {
            _saveFolderPath = Path.Combine(Application.dataPath, "..", FolderName);
            _saveFolderPath = Path.GetFullPath(_saveFolderPath);

            if (!Directory.Exists(_saveFolderPath))
            {
                Directory.CreateDirectory(_saveFolderPath);
            }
        }

        public static void CreateJsonFile<T>(T gameData)
        {
            if (_saveFolderPath == null ||!Directory.Exists(_saveFolderPath))
            {
                CreatFolder();
            }

            int counter = 1;
            string createFilePath;
            do
            {
                createFilePath = Path.Combine(_saveFolderPath, $"{BaseFileName}{counter}.json");
                counter++;
            }
            while (File.Exists(createFilePath));

            string json = JsonUtility.ToJson(gameData, true);
            File.WriteAllText(createFilePath, json);
        }

        public static bool CreateJsonFile<T>(T gameData,string fileName)
        {
            if (!Directory.Exists(_saveFolderPath))
            {
                CreatFolder();
            }

            string createFilePath = Path.Combine(_saveFolderPath, $"{fileName}.json");
            if (File.Exists(createFilePath))
            {
                return false;
            }

            string json = JsonUtility.ToJson(gameData, true);
            File.WriteAllText(createFilePath, json);
            return true;
        }
        
        public static string[] CanReadFilePaths() => Directory.GetFiles(_saveFolderPath);

        public static T ReadFile<T>(string path)
        {
            if (!File.Exists(path))
            {
                Debug.Log("path Error");
                return default;
            }

            string json = File.ReadAllText(path);
            return JsonUtility.FromJson<T>(json);
        }
        
    }
}

