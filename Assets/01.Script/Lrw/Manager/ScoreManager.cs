using System;
using UnityEngine;

namespace _01.Script.Lrw.Manager
{
    public class ScoreManager : MonoBehaviour,ISingleton
    {
        private static ScoreManager _instance;

        private void Singleton()
        {
            if (_instance == null)
            {
                _instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }
        private void Awake()
        {
            
            
            
        }


        public void SingletonDestroy()
        {
            Destroy(gameObject);
        }
    }
}