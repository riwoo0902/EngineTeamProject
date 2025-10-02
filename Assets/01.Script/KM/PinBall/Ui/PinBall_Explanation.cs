using Lrw_PinBall;
using TMPro;
using UnityEngine;

public class PinBall_Explanation : MonoBehaviour
{
    public static PinBall_Explanation Instance;
    [SerializeField] private TMP_Text pinBallName;
    [SerializeField] private TMP_Text pinBall_Explanation;
    
    [Space(5)]
    [Header("PinBall Values")]
    [SerializeField] private TMP_Text pinBall_Mass;
    [SerializeField] private TMP_Text pinBall_Friction;
    [SerializeField] private TMP_Text pinBall_Bounciless;

    
    private string _pinBallName;
    private string _pinBall_Explanation;
    private float _pinBall_Mass;
    private float _pinBall_Friction;
    private float _pinBall_Bounciless;
    private void Start()
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

    public void PointerOnEnter(PinBallSO pinball)
    {
        SettingSOinUi(pinball);
    }

    public void PointerOnExit()
    {

    }

    private void SettingSOinUi(PinBallSO pinball)
    {
        _pinBallName = pinball.BallName;
        _pinBall_Explanation = pinball.BallExplanation;
        _pinBall_Mass = pinball.Friction;
        _pinBall_Friction = pinball.Mass;
        _pinBall_Bounciless = pinball.Bounciness;

        pinBallName.text = _pinBallName;
        pinBall_Explanation.text = _pinBall_Explanation;
        pinBall_Mass.text = _pinBall_Mass.ToString();
        pinBall_Friction.text = _pinBall_Friction.ToString();
        pinBall_Bounciless.text = _pinBall_Bounciless.ToString();
    }
}
