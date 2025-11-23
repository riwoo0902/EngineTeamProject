using UnityEngine;

public class SaveStageData : MonoBehaviour
{
    public MapType typeThis;

    public void ChoiceStage()
    {
        StageManager.Instance.SceneChange(typeThis);
    }
}
