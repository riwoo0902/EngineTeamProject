using DG.Tweening;
using UnityEngine;

public class LineSetting : MonoBehaviour
{
    [SerializeField] private GameObject EndPoint;
    [SerializeField] private float duration = 2f;
    private LineRenderer _lineCompo;

    private void Start()
    {
        _lineCompo = GetComponent<LineRenderer>();
        _lineCompo.SetPosition(0, new Vector3(transform.position.x, transform.position.y, 0));
        _lineCompo.positionCount = 20;
        Vector3 poss = new Vector3(EndPoint.transform.position.x, EndPoint.transform.position.y, 0);
        for (int i = 0; i < _lineCompo.positionCount - 1; i++)
        {
            Vector3 midPos = Vector3.Lerp(_lineCompo.GetPosition(0), poss, (i + 1) / 20f);
            _lineCompo.SetPosition(i + 1, midPos);
        }
    }

    [ContextMenu("Test Line")]
    private void LineTest()
    {
        Gradient gri = new Gradient();
        GradientColorKey[] colorKey = new GradientColorKey[3];
        GradientAlphaKey[] alphaKey = new GradientAlphaKey[2];
        colorKey[0].color = Color.black;
        colorKey[0].time = 0f;
        colorKey[1].color = Color.white;
        colorKey[1].time = 0.001f;
        colorKey[2].color = Color.white;
        colorKey[2].time = 1f;
        alphaKey[0].alpha = 255f;
        alphaKey[0].time = 0f;
        alphaKey[1].alpha = 255f;
        alphaKey[1].time = 1f;
        gri.SetKeys(colorKey, alphaKey);
        DOTween.To(() => 0f, x =>
        {
            _lineCompo.colorGradient = gri;
            gri.SetKeys(colorKey, gri.alphaKeys);
            colorKey[0].time = x;
            colorKey[1].time = x;
        }, 1f,duration).SetEase(Ease.Linear);
    }
}