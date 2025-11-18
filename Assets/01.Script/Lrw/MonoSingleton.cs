using System;
using UnityEngine;

namespace _01.Script.Lrw
{
    public abstract class MonoSingleton :MonoBehaviour
    {
        public static MonoSingleton Instance { get; private set; }
        protected virtual void Awake()
        {
            Singleton();
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
    }
}