using Lrw_PinBall;
using TMPro;
using UnityEngine;

public class FireDataInPinBall : MonoBehaviour
{
    public PinBallSO NowPinBall;

    [SerializeField] private TMP_Text nameText;
    private PinBall_Explanation _pinball_Ex;

    private void Start()
    {
        _pinball_Ex = PinBall_Explanation.Instance;
        nameText.text = NowPinBall.BallName;
    }

    public void SetData()
    {
        nameText.text = NowPinBall.BallName;
    }

    public void FireOnEnter()
    {
        _pinball_Ex.PointerOnEnter(NowPinBall);
    }

    public void FireOnExit()
    {
        _pinball_Ex.PointerOnExit();
    }
}
