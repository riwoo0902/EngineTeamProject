using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveStageData : MonoBehaviour
{
    public MapType typeThis;
    [SerializeField] private Image imgae;
    [SerializeField] private List<Sprite> stageSprites = new List<Sprite>();

    private void Start()
    {
        imgae.sprite = stageSprites[(int)typeThis - 1];
    }

    public void ChoiceStage()
    {
        StageManager.Instance.SceneChange(typeThis);
        MapManager.Instance.Save();
    }
}
