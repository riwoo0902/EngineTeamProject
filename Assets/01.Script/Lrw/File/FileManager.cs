using System.IO;
using UnityEngine;

namespace _01.Script.Lrw.File
{
    public static class FileManager
    {
        private static readonly string FolderName = "GameDataFolder";
        private static readonly string BaseFileName = "GameDataFile";

        private static string _saveFolderPath;

        private static void CreatFolder()
        {
            _saveFolderPath = Path.Combine(Application.dataPath, "..", FolderName);
            _saveFolderPath = Path.GetFullPath(_saveFolderPath);

            if (!Directory.Exists(_saveFolderPath))
            {
                Directory.CreateDirectory(_saveFolderPath);
            }
        }

        public static void SetFile(string gameData)
        {
            if (_saveFolderPath == null ||!Directory.Exists(_saveFolderPath))
            {
                CreatFolder();
            }

            int counter = 1;
            string createFilePath;
            do
            {
                createFilePath = Path.Combine(_saveFolderPath, $"{BaseFileName}{counter}.txt");
                counter++;
            }
            while (System.IO.File.Exists(createFilePath));
            
            System.IO.File.WriteAllText(createFilePath, gameData);
        }

        public static void SetFile(string gameData,string fileName)
        {
            if (!Directory.Exists(_saveFolderPath))
            {
                CreatFolder();
            }

            string createFilePath = Path.Combine(_saveFolderPath, $"{fileName}.txt");
            
            System.IO.File.WriteAllText(createFilePath, gameData);
        }
        
        public static string[] CanReadFilePaths() => Directory.GetFiles(_saveFolderPath);

        public static string ReadFile(string path)
        {
            if (!System.IO.File.Exists(path))
            {
                Debug.Log("path Error");
                return string.Empty;
            }

  
            return System.IO.File.ReadAllText(path);
        }

        public static string GetFilePath(string fileName)
        {
            CreatFolder();
            return Path.Combine(_saveFolderPath, $"{fileName}.txt");
        }
        
        
    }
}

