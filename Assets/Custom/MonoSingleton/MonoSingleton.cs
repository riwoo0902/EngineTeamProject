using System;
using UnityEngine;

namespace Custom.MonoSingleton
{
    public abstract class MonoSingleton<T> : MonoBehaviour where T : class
    {
        [SerializeField] private SingletonSetting singletonSetting;
        public static T Instance { get; private set; }
        protected virtual void Awake()
        {
            Singleton();
        }
        
        private void Singleton()
        {
            if (Instance == null)
            {
                Instance = this as T;
                if (singletonSetting.DontDestroyLoadObject)
                {
                    DontDestroyOnLoad(gameObject);
                }
            }
            else
            {
                Destroy(gameObject);
            }
        }

        protected virtual void OnDestroy()
        {
            Instance = null;
        }

        [Serializable]
        public class SingletonSetting
        {
            [field:SerializeField] public bool DontDestroyLoadObject { get; private set; } = false;
        }
    }
}