using System.Collections.Generic;
using Lrw_PinBall;
using UnityEngine;

public class PinballResner : MonoBehaviour
{
    [SerializeField] private GameObject pinBallUI;

    private PinballManager _pinballManager;
    private List<GameObject> _pinballs = new List<GameObject>();

    private void Start()
    {
        _pinballManager = PinballManager.Instance;

        _pinballManager.AddPinBall += AddPinBallInUi;
        _pinballManager.RemovePinBall += RemovePinBallInUi;
    }

    private void OnDestroy()
    {
        _pinballManager.AddPinBall -= AddPinBallInUi;
        _pinballManager.RemovePinBall -= RemovePinBallInUi;
    }

    private void AddPinBallInUi(PinBallSO pinball)
    {
        GameObject pinballObj = Instantiate(pinBallUI, transform);
        FireDataInPinBall pinballData = pinballObj.GetComponent<FireDataInPinBall>();
        pinballData.NowPinBall = pinball;
        pinballData.SetData();
        _pinballs.Add(pinballObj);
    }

    private void RemovePinBallInUi(PinBallSO pinball)
    {
        for (int i = 0; i < _pinballs.Count; i++)
        {
            if (_pinballs[i].GetComponent<FireDataInPinBall>().NowPinBall == pinball)
            {
                GameObject pinballObj = _pinballs[i];
                _pinballs.Remove(pinballObj);
                Destroy(pinballObj);
                break;
            }
        }
    }
}
