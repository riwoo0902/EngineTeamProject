using System.IO;
using UnityEngine;

namespace Lrw_PinBall
{
    public class PinBallStageFileManager : MonoBehaviour
    {
        private readonly string _folderName = "PinBallStageFolder";
        private readonly string _BaseFileName = "PinBallStage";

        private string _saveFolderPath;

        public static PinBallStageFileManager instance { get; private set; }
        private void Awake()
        {
            #region
            if (instance != null)
            {
                Destroy(gameObject);
                return;
            }
            else
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
            #endregion

            CreatFolder();
        }

        public static bool GetInstance(out PinBallStageFileManager returnInstance)
        {
            returnInstance = instance;
            return instance != null;
        }

        public void CreatFolder()
        {
            _saveFolderPath = Path.Combine(Application.dataPath, "..", _folderName);
            _saveFolderPath = Path.GetFullPath(_saveFolderPath);

            if (!Directory.Exists(_saveFolderPath))
            {
                Directory.CreateDirectory(_saveFolderPath);
                Debug.Log("폴더생성\n폴더 주소 : " + _saveFolderPath);
            }
            else
            {
                Debug.Log("동일한 폴더가 존재합니다.\n폴더 주소 : " + _saveFolderPath);
            }
        }
        
        public void CreateJsonFile<T>(T gameData, out string createFilePath)
        {
            if (!Directory.Exists(_saveFolderPath))
            {
                CreatFolder();
            }

            int counter = 1;

            do
            {
                createFilePath = Path.Combine(_saveFolderPath, $"{_BaseFileName}{counter}.json");
                counter++;
            }
            while (File.Exists(createFilePath));

            string json = JsonUtility.ToJson(gameData, true);
            File.WriteAllText(createFilePath, json);
            Debug.Log("파일생성 성공\n파일 주소 : " + createFilePath);

        }



        public string[] CanReadFilePath() => Directory.GetFiles(_saveFolderPath);


        public T ReadFile<T>(T type, string path)
        {
            if (!Directory.Exists(path))
            {
                Debug.Log("path Error");
                return default;
            }

            var loadedJson = Resources.Load<TextAsset>(path);
            return JsonUtility.FromJson<T>(loadedJson.ToString());
        }



    }
}

