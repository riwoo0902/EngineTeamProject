using Lrw_PinBall;
using UnityEngine;

public class PinballResner : MonoBehaviour
{
    private PinballManager _pinballManager;

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

    }
    
    private void RemovePinBallInUi(PinBallSO pinball)
    {
        
    }
}
