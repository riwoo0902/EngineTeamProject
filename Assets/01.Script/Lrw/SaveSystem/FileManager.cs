using System.IO;
using UnityEngine;

namespace _01.Script.Lrw.SaveSystem
{
    public static class FileManager
    {
        private static readonly string FolderName = "GameDataFolder";
        private static readonly string BaseFileName = "GameDataFile";

        private static string _saveFolderPath;

        public static void PathClear()
        {
            _saveFolderPath = "";
        }
        private static void CreatFolder()
        {
            _saveFolderPath = Path.Combine(Application.dataPath, "..", FolderName);
            _saveFolderPath = Path.GetFullPath(_saveFolderPath);

            if (!Directory.Exists(_saveFolderPath))
            {
                Directory.CreateDirectory(_saveFolderPath);
            }
        }
        
        public static void SetFile(string fileName,string gameData)
        {
            if (!Directory.Exists(_saveFolderPath))
            {
                CreatFolder();
            }

            string createFilePath = Path.Combine(_saveFolderPath, $"{fileName}.txt");
            
            File.WriteAllText(createFilePath, gameData);
        }
        
        public static string[] CanReadFilePaths() => Directory.GetFiles(_saveFolderPath);

        public static string ReadFile(string path)
        {
            if (!File.Exists(path))
            {
                Debug.Log("path Error");
                return string.Empty;
            }
            
            return File.ReadAllText(path);
        }

        public static string GetFilePath(string fileName)
        {
            CreatFolder();
            return Path.Combine(_saveFolderPath, $"{fileName}.txt");
        }
        
        
    }
}

