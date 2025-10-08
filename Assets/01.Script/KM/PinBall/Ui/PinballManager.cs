using System;
using Lrw_PinBall;
using UnityEngine;

public class PinballManager : MonoBehaviour
{
    public static PinballManager Instance;
    public Action<PinBallSO> OnChangedPinBall;
    public Action<PinBallSO> AddPinBall;
    public Action<PinBallSO> RemovePinBall;
    private void Awake()
    {
        if (Instance)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    public void ChangeNowPinBall(PinBallSO targetPinBall)
    {
        OnChangedPinBall?.Invoke(targetPinBall);
    }
}
