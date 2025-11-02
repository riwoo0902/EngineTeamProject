using System;
using _01.Script.Lrw.EventBus.EventBusSystem.CoreSystem;
using _01.Script.Lrw.EventBus.EventBusSystem.Events;
using Lrw_Input;
using UnityEngine;

namespace _01.Script.Lrw.GameManager
{
    public class GameManager : MonoBehaviour
    {
        [field:SerializeField] public InputSO InputSo { get; private set; }
        private Vector2 _pevMousePos;
        
        public static GameManager Instance { get; private set; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Update()
        {
            if (InputSo.MousePos != _pevMousePos)
            {
                EventBus<MousePosEvent>.Raise(new MousePosEvent(InputSo.MouseScreenPos,InputSo.MousePos));
            }
            
            
        }
    }
}