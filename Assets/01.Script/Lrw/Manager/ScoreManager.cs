using System.Globalization;
using _01.Script.Lrw.EventBus.EventBusSystem.CoreSystem;
using _01.Script.Lrw.EventBus.EventBusSystem.Events;
using _01.Script.Lrw.File;
using Lrw_CustomReadonly;
using UnityEngine;

namespace _01.Script.Lrw.Manager
{
    [DefaultExecutionOrder(-10)]
    public class ScoreManager : MonoBehaviour
    {
        public static ScoreManager Instance { get; private set; }
        [SerializeField,ReadOnly] private float score;
        public float Score
        {
            get => score;
            set => score = value;
        }
        
        private void Awake()
        {
            Singleton();
            SetScoreData();
            EventBus<ScoreAddEvent>.OnEvent += AddScore;
        }
        
        private void AddScore(ScoreAddEvent scoreAddEvent)
        {
            score += scoreAddEvent.AddScore;
        }

        private void Singleton()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }
        
        private void SetScoreData()
        {
            string i = FileManager.ReadFile(FileManager.GetFilePath("Score"));
            Score = (int.TryParse(i,out int j) ? j : 0);
        }
        

        private void OnDestroy()
        {
            EventBus<ScoreAddEvent>.OnEvent -= AddScore;
            FileManager.SetFile(Score.ToString(),"Score");
        }
        

    }
}