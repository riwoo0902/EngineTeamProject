using _01.Script.Lrw.PinBallMap;
using UnityEngine;

public class MoveDirTest : MonoBehaviour
{
    [ContextMenu("Left")]
    public void MoveLeft()
    {
        MapManager.Instance.OnMapeDir?.Invoke(MapDir.Left);
    }
    [ContextMenu("Right")]
    public void MoveRight()
    {
        MapManager.Instance.OnMapeDir?.Invoke(MapDir.Right);
    }
}
