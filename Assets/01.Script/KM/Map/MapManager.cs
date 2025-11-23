using System;
using System.Collections.Generic;
using _01.Script.Lrw.PinBallMap;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public static MapManager Instance;
    public Action<MapDir> OnMapeDir;

    [SerializeField] private CreateStageTree stageTree;
    [SerializeField] private Transform playerMarker;

    private GameObject _currentStage;

    private int _currentFloorIndex;
    private int _currentStageIndex;
    private bool _isMoving = false;

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

    private void Start()
    {
        var data = MapSaveSystem.Load();
        if (data != null)
        {
            stageTree.Generate(data.rand);
            RestoreStageTypes(data);
            if (data.currentFloorIndex >= 0 &&
                data.currentFloorIndex < stageTree.Stages.Count &&
                data.currentStageIndex >= 0 &&
                data.currentStageIndex < stageTree.Stages[data.currentFloorIndex].Count)
            {
                _currentFloorIndex = data.currentFloorIndex;
                _currentStageIndex = data.currentStageIndex;
                _currentStage = stageTree.Stages[_currentFloorIndex][_currentStageIndex];
            }
            else
            {
                InitToRoot();
            }
        }
        else
        {
            stageTree.Generate(null);
            InitToRoot();
        }

        UpdateMarkerPosition();
    }

    private void InitToRoot()
    {
        if (stageTree.Stages.Count > 0 && stageTree.Stages[0].Count > 0)
        {
            _currentFloorIndex = 0;
            _currentStageIndex = 0;
            _currentStage = stageTree.Stages[0][0];
        }
        else
        {
            _currentStage = null;
        }
    }

    private void OnApplicationQuit()
    {
        Save();
    }

    public void Save()
    {
        if (_currentStage == null || stageTree.TotalFloors <= 0)
            return;

        var data = new MapSaveData
        {
            rand = stageTree.RandValue,
            currentFloorIndex = _currentFloorIndex,
            currentStageIndex = _currentStageIndex
        };
        var typeList = new List<int>();
        for (int f = 0; f < stageTree.Stages.Count; f++)
        {
            var list = stageTree.Stages[f];
            for (int i = 0; i < list.Count; i++)
            {
                var ssd = list[i].GetComponent<SaveStageData>();
                if (ssd != null)
                    typeList.Add((int)ssd.typeThis);
                else
                    typeList.Add((int)MapType.None);
            }
        }

        data.stageTypes = typeList.ToArray();

        MapSaveSystem.Save(data);
    }

    private void RestoreStageTypes(MapSaveData data)
    {
        if (data.stageTypes == null) return;

        int idx = 0;
        for (int f = 0; f < stageTree.Stages.Count; f++)
        {
            var list = stageTree.Stages[f];
            for (int i = 0; i < list.Count; i++)
            {
                if (idx >= data.stageTypes.Length)
                    return;

                var ssd = list[i].GetComponent<SaveStageData>();
                if (ssd != null)
                {
                    ssd.typeThis = (MapType)data.stageTypes[idx];
                }

                idx++;
            }
        }
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

    [ContextMenu("Reset Map")]
    public void ResetMap()
    {
        MapSaveSystem.Delete();
        _isMoving = false;

        stageTree.Generate(null);
        InitToRoot();
        UpdateMarkerPosition();
    }

    private void MapMove(MapDir dir)
    {
        if (_isMoving) return;

        if (_currentStage == null)
        {
            InitToRoot();
            UpdateMarkerPosition();
        }

        if (_currentStage == null)
        {
            return;
        }
        LineSetting[] lines = _currentStage.GetComponentsInChildren<LineSetting>(true);
        LineSetting selectedLine = null;

        foreach (var line in lines)
        {
            if (line != null && line.Dir == dir)
            {
                selectedLine = line;
                break;
            }
        }

        if (selectedLine == null)
        {
            return;
        }

        _isMoving = true;

        GameObject nextStage = selectedLine.EndPoint;
        if (nextStage == null)
        {
            _isMoving = false;
            return;
        }
        _currentStage = nextStage;
        UpdateCurrentIndicesFromStage(_currentStage);
        UpdateMarkerPosition();

        selectedLine.LineMove();
    }

    private void UpdateCurrentIndicesFromStage(GameObject stage)
    {
        for (int f = 0; f < stageTree.Stages.Count; f++)
        {
            var list = stageTree.Stages[f];
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i] == stage)
                {
                    _currentFloorIndex = f;
                    _currentStageIndex = i;
                    return;
                }
            }
        }
    }

    private void UpdateMarkerPosition()
    {
        if (playerMarker != null && _currentStage != null)
        {
            playerMarker.position = _currentStage.transform.position;
        }
    }
}
