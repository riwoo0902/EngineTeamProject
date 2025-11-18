using System;
using UnityEngine;

namespace _01.Script.Lrw
{
    public abstract class MonoSingleton : MonoBehaviour
    {
        [SerializeField] private SingletonSetting singletonSetting;
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
        [Serializable]
        public class SingletonSetting
        {
            [field:SerializeField] public bool DontDestroyLoadObject { get; private set; } = false;
        }
    }
}