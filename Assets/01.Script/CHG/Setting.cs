using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Setting : MonoBehaviour
{
    [SerializeField] private Button SettingBtn;

    private Vector3 Scale;
    private Tween tween;

    private void Start()
    {
        Scale = transform.localScale;
        transform.localScale = Vector3.zero;
        SettingBtn.onClick.AddListener(SettingShow);
        SettingBtn.onClick.AddListener(() => Debug.Log("aaa"));
    }
    public void SettingShow()
    {
        tween?.Kill();

        tween =  gameObject.transform.DOScale(Scale, 0.3f);
    }

    public void SettingHide()
    {
        tween?.Kill();

        tween = gameObject.transform.DOScale(Vector3.zero, 0.3f);
    }

    public void GameEnd()
    {
        Application.Quit();
    }


}
