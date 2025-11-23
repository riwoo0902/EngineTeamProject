using System;
using _01.Script.Lrw.Manager;
using _01.Script.Lrw.PinBallCompo;
using _01.Script.Lrw.PinBallCompo.FSM;
using _01.Script.Lrw.UI.PinBalls;
using Custom.MonoSingleton;
using Lrw_PinBall;
using UnityEngine;

namespace _01.Script.Lrw.PinBallMap
{
    public class PinBallSpawner : MonoSingleton<PinBallSpawner>
    {
        [SerializeField] private PinBallBase currentPinBall;
        private GameObject pinBallGameObject;

        protected override void Awake()
        {
            base.Awake();
            

        }
        
        [ContextMenu("Spawn")]
        public void PinBallSpawn()
        {
            GameManager.Instance.state = PinBallStates.Idle;
            if (pinBallGameObject != null)
            {
                Destroy(pinBallGameObject);
                pinBallGameObject = null;
            }
            if (PinBallUIManager.Instance.CurrentHavePinBalls.Count == 0)
            {
                SlotNull();
                return;
            }
            
            try
            {
                PinBallSO pinBallSo = PinBallUIManager.Instance.CurrentHavePinBalls[0];
                if (pinBallSo == null) throw new Exception("pinBallSo is null");
                if (pinBallSo.BallPrefab == null) throw new Exception("pinBallSo.BallPrefab is null");
                if (pinBallSo.PinBallImage == null) throw new Exception("pinBallSo.PinBallImage is null");

                pinBallGameObject = Instantiate(pinBallSo.BallPrefab, transform);
                
                PinBallBase pinBallBase = pinBallGameObject.GetComponent<PinBallBase>();
                if (pinBallBase == null) throw new Exception("pinBallBase is null");

                pinBallBase.SetPinBallSo(pinBallSo);
                currentPinBall = pinBallBase;
            }
            catch(Exception e)
            {
                Debug.Log(e.Message);
                currentPinBall = null;
                if(pinBallGameObject != null) Destroy(pinBallGameObject);
            }
        }

        private void SlotNull()
        {
            Debug.Log("PinBall All Use");
        }
        
    }
}