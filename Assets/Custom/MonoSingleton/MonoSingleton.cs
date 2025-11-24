using System;
using UnityEngine;

namespace Custom.MonoSingleton
{
    public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
    {
        [SerializeField] private SingletonSetting singletonSetting;
        public static T Instance { get; private set; }  
        protected virtual void Awake()
        {
            InitializeSingleton();
        }
        
        private void InitializeSingleton()
        {
            if (Instance == null)
            {
                Instance = this as T;
                
                if (singletonSetting.DontDestroyLoadObject)
                {
                    transform.SetParent(null);
                    DontDestroyOnLoad(gameObject);
                }
            }
            else if (Instance != this)
            {
                Destroy(gameObject);
            }
        }

        protected virtual void OnDestroy()
        {
            if (Instance == this)
            {
                Instance = null;
            }
        }

        [Serializable]
        public class SingletonSetting
        {
            [field:SerializeField] public bool DontDestroyLoadObject { get; private set; } = false;
        }
    }
}