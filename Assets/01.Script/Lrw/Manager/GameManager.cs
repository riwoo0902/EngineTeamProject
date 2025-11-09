using System;
using _01.Script.Lrw.EventBus.EventBusSystem.CoreSystem;
using _01.Script.Lrw.EventBus.EventBusSystem.Events;
using _01.Script.Lrw.PinBallCompo.FSM;
using Lrw_Input;
using UnityEngine;

namespace _01.Script.Lrw.Manager
{
    public class GameManager : MonoBehaviour
    {
        [field:SerializeField] public InputSO InputSo { get; private set; }
        private Vector2 _pevMousePos;
        public PinBallStates state = PinBallStates.Idle;
        
        public static GameManager Instance { get; private set; }

        private void Awake()
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

        private void Update()
        {
            EventBus<MousePosEvent>.Raise(new MousePosEvent(InputSo.MouseScreenPos,InputSo.MousePos));
            
        }
        
    }
}