using UnityEditor;
using UnityEngine;

namespace _01.Script.Lrw
{
    public static class Singleton<T> where T : ISingleton
    {
        private static T _instance;
        public static T Instance
        {
            get
            {
                return _instance;
            }
            set
            {
                if(_instance ==  null) _instance =  value;
                else value.SingletonDestroy();
            }
        }
        
    }
}