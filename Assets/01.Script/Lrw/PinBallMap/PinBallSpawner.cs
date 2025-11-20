using System;
using _01.Script.Lrw.PinBallCompo;
using Lrw_PinBall;
using UnityEngine;

namespace _01.Script.Lrw.PinBallMap
{
    public class PinBallSpawner : MonoBehaviour
    {
        public static PinBallSpawner Instance { get; private set; }
        [SerializeField] private PinBallBase currentPinBall;
        
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
        private void Awake()
        {
            Singleton();

        }

        public void PinBallSpawn(PinBallSO pinBallSo)
        {
            try
            {
                if (pinBallSo == null) throw new Exception("pinBallSo is null");
                if (pinBallSo.BallPrefab == null) throw new Exception("pinBallSo.BallPrefab is null");
                if(pinBallSo.PinBallImage == null) throw new Exception("pinBallSo.PinBallImage is null"); 
                
                GameObject pinBallGameObject = Instantiate(pinBallSo.BallPrefab, transform);
                
                PinBallBase pinBallBase = pinBallGameObject.GetComponent<PinBallBase>();
                if (pinBallBase == null) throw new Exception("pinBallBase is null");
                
                pinBallBase.SetPinBallSo(pinBallSo);
                currentPinBall = pinBallBase;
            }
            catch(Exception e)
            {
                Debug.Log(e.Message);
                return;
            }
            
            
        }
        
    }
}