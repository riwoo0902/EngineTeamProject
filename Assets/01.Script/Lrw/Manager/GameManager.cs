using _01.Script.Lrw.EventBus.EventBusSystem.CoreSystem;
using _01.Script.Lrw.EventBus.EventBusSystem.Events;
using _01.Script.Lrw.PinBallCompo.FSM;
using Lrw_Input;
using UnityEngine;

namespace _01.Script.Lrw.Manager
{
    [DefaultExecutionOrder(-20)]
    public class GameManager : MonoBehaviour
    {
        [field:SerializeField] public InputSO InputSo { get; private set; }
        public PinBallStates state = PinBallStates.None;
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
                return;
            }
            
            InputSo.Initialize();
            
        }

        private void Update()
        {
            EventBus<MousePosEvent>.Raise(new MousePosEvent(InputSo.MouseScreenPos,InputSo.MousePos));
            
        }


        private void OnDestroy()
        {
            InputSo.Cleanup();
        }
        
        private void OnApplicationQuit()
        {
            InputSo.Cleanup();
        }
    }
}