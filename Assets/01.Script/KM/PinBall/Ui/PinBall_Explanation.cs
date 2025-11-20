using _01.Script.Lrw.UI.PinBalls;
using TMPro;
using UnityEngine;

public class PinBall_Explanation : MonoBehaviour
{
    public static PinBall_Explanation Instance { get; private set; }
    [SerializeField] private TMP_Text pinBallName;
    [SerializeField] private TMP_Text pinBall_Explanation;
    
    [Header("PinBall Values")]
    [SerializeField] private TMP_Text pinBall_Mass;
    [SerializeField] private TMP_Text pinBall_Friction;
    [SerializeField] private TMP_Text pinBall_Bounciless;
    [SerializeField] private float spaceDistance = 5f;
    
    [SerializeField] private PinBallUISetting pinBallUISetting;
    
    [SerializeField] private Vector2 offset;
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

    public void PointerOnEnter(PinBallUISetting pinball,Transform target)
    {
        _canvasGroup.alpha = 1;
        SettingSOinUi(pinball);
        _rectCompo.position = target.position;
        _rectCompo.position += (Vector3)offset;

    }
    
    public void PointerOnExit()
    {
        _canvasGroup.alpha = 0;
    }

    private void SettingSOinUi(PinBallUISetting pinball)
    {
        pinBallUISetting =  pinball;

        pinBallName.text = pinBallUISetting.pinBallName;
        pinBall_Explanation.text = pinBallUISetting.pinBallExplanation;
        pinBall_Mass.text = pinBallUISetting.pinBallMass;
        pinBall_Friction.text = pinBallUISetting.pinBallFriction;
        pinBall_Bounciless.text = pinBallUISetting.pinBallBounce;
    }
}
