using System;
using _01.Script.Lrw.PinBallMap;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public static MapManager Instance;
    public Action<MapDir> OnMapeDir;

    [SerializeField] private CreateStageTree stageTree;
    [SerializeField] private Transform playerMarker;

    private GameObject _currentStage;

    private void Awake()
    {
        if (Instance)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        OnMapeDir += MapMove;
    }


    [ContextMenu("Left")]
    public void MoveLeft()
    {
        OnMapeDir?.Invoke(MapDir.Left);
    }
    [ContextMenu("Right")]
    public void MoveRight()
    {
        OnMapeDir?.Invoke(MapDir.Right);
    }

    private void MapMove(MapDir dir)
    {
        if (_currentStage == null)
        {
            if (stageTree.Stages.Count > 0 && stageTree.Stages[0].Count > 0)
            {
                _currentStage = stageTree.Stages[0][0];
                if (playerMarker) playerMarker.position = _currentStage.transform.position;
            }
        }
        LineSetting[] lines = _currentStage.GetComponentsInChildren<LineSetting>(true);
        LineSetting selected = null;
        foreach (var line in lines)
        {
            GameObject end = line.EndPoint;
            if (dir == MapDir.Right)
            {
                selected = line;
                break;
            }
            if (dir == MapDir.Left)
            {
                selected = line;
                break;
            }
        }

        if (selected == null) Debug.Log("Null");
        selected.LineMove();
        _currentStage = selected.EndPoint;
        if (playerMarker != null)
        {
            playerMarker.position = _currentStage.transform.position;
        }
    }
}
