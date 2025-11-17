using Lrw_PinBall;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PinBall_Explanation : MonoBehaviour
{
    public static PinBall_Explanation Instance { get; private set; }
    [SerializeField] private TMP_Text pinBallName;
    [SerializeField] private TMP_Text pinBall_Explanation;
    
    [Space(5)]
    [Header("PinBall Values")]
    [SerializeField] private TMP_Text pinBall_Mass;
    [SerializeField] private TMP_Text pinBall_Friction;
    [SerializeField] private TMP_Text pinBall_Bounciless;
    [SerializeField] private float spaceDistance = 5f;
    
    private string _pinBallName;
    private string _pinBall_Explanation;
    private float _pinBall_Mass;
    private float _pinBall_Friction;
    private float _pinBall_Bounciless;

    private RectTransform _rectCompo;
    private CanvasGroup  _canvasGroup;
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
        _canvasGroup = GetComponent<CanvasGroup>();
        _rectCompo = GetComponent<RectTransform>();
        PointerOnExit();
    }

    public void PointerOnEnter(PinBallSO pinball,Transform target)
    {
        _canvasGroup.alpha = 1;
        SettingSOinUi(pinball);
        _rectCompo.position = target.position;
        _rectCompo.position = new Vector3(_rectCompo.position.x + spaceDistance, _rectCompo.position.y, 0);

    }

    public void PointerOnExit()
    {
        _canvasGroup.alpha = 0;
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
