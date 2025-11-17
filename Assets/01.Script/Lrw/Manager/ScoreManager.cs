using _01.Script.Lrw.File;
using Lrw_CustomReadonly;
using UnityEngine;

namespace _01.Script.Lrw.Manager
{
    [DefaultExecutionOrder(-10)]
    public class ScoreManager : MonoBehaviour
    {
        public static ScoreManager Instance { get; private set; }
        [SerializeField,ReadOnly] private int score;
        public int Score
        {
            get => score;
            set => score = value;
        }
        
        private void Awake()
        {
            Singleton();
            SetScoreData();

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
            FileManager.SetFile(Score.ToString(),"Score");
        }
        

    }
}