using System;
using System.Collections.Generic;
using Lrw_PinBall;
using UnityEngine;

public class PinballManager : MonoBehaviour
{
    public static PinballManager Instance;
    public Action<PinBallSO> OnChangedPinBall;
    public Action<PinBallSO> AddPinBall;
    public Action<PinBallSO> RemovePinBall;

    public List<PinBallSO> testSOs = new List<PinBallSO>();
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

    [ContextMenu("Fire")]
    private void FireTest()
    {
        for (int i = 0; i < testSOs.Count; i++)
        {
            AddPinBall?.Invoke(testSOs[i]);
        }
    }

    [ContextMenu("Remove")]
    private void RemoveTest()
    {
        for (int i = 0; i < testSOs.Count; i++)
        {
            RemovePinBall?.Invoke(testSOs[i]);
        }
    }

    public void ChangeNowPinBall(PinBallSO targetPinBall)
    {
        OnChangedPinBall?.Invoke(targetPinBall);
    }
}
