using UnityEngine;

public class CBattleLoad : MonoBehaviour
{
    public void ButtonClick()
    {
        //StageManager.Instance.SceneChange(MapType.Event);
        //StageManager.Instance.SceneChange(MapType.Store);
        StageManager.Instance.SceneChange(MapType.Battle);
    }
}
