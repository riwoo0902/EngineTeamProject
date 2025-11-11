using System;
using _01.Script.Lrw.PinBallMap;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public static MapManager Instance;
    public Action<MapDir> OnMapeDir;

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

    private void MapMove(MapDir dir)
    {

    }
}