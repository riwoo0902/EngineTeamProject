using System.Collections.Generic;
using UnityEngine;
using _01.Script.Lrw.PinBallMap;

public class CreateStageTree : MonoBehaviour
{
    [SerializeField] private int percent = 10;
    [SerializeField] private int countZeropercent = 3;
    [SerializeField] private GameObject StagePrefab;
    [SerializeField] private GameObject LinePrefab;
    [SerializeField] private GameObject cameraObj;
    [SerializeField] private GameObject Parent;

    [SerializeField] private int PercentByNormalStage = 50;
    [SerializeField] private int PercentByShopStage = 10;
    [SerializeField] private int PercentByEventStage = 10;
    [SerializeField] private int PercentByChoiceStage = 30;

    [SerializeField] private float horizontalSpacing = 3f;
    [SerializeField] private float verticalSpacing = 3f;
    [SerializeField] private Vector3 origin = Vector3.zero;
    public List<List<GameObject>> Stages { get; private set; } = new List<List<GameObject>>();

    public int RandValue { get; private set; }
    public int TotalFloors { get; private set; }

    [ContextMenu("CreateStages")]
    private void CreateStages()
    {
        Generate(null);
    }
    public void Generate(int? forcedRand)
    {
        Stages.Clear();

        if (Parent != null)
        {
            for (int i = Parent.transform.childCount - 1; i >= 0; i--)
            {
                DestroyImmediate(Parent.transform.GetChild(i).gameObject);
            }
        }
        int rand = forcedRand ?? Random.Range(0, 100);
        RandValue = rand;

        int extraFloors = 0;
        int tempPercent = 0;

        while (tempPercent < rand)
        {
            tempPercent += percent;
            extraFloors++;
        }

        int totalFloors = countZeropercent + extraFloors;
        if (totalFloors <= 0)
        {
            TotalFloors = 0;
            return;
        }

        if (totalFloors % 2 == 0)
        {
            if (extraFloors > 0) totalFloors--;
            else totalFloors++;
        }

        if (totalFloors <= 0)
        {
            TotalFloors = 0;
            return;
        }

        TotalFloors = totalFloors;
        int midFloor = (TotalFloors + 1) / 2;
        for (int floorIndex = 0; floorIndex < TotalFloors; floorIndex++)
        {
            int floorNumber = floorIndex + 1;
            int nodeCount;

            if (floorNumber <= midFloor)
            {
                nodeCount = floorNumber;
            }
            else
            {
                nodeCount = TotalFloors - floorNumber + 1;
            }

            var floorList = new List<GameObject>();

            float startX = -((nodeCount - 1) * 0.5f) * horizontalSpacing;
            float y      = origin.y - (floorNumber * verticalSpacing);

            for (int i = 0; i < nodeCount; i++)
            {
                float x = origin.x + startX + (i * horizontalSpacing);
                Vector3 pos = new Vector3(x, y, 0f);

                GameObject stage = Instantiate(
                    StagePrefab, pos, Quaternion.identity,
                    Parent != null ? Parent.transform : transform
                );

                var saveData = stage.GetComponent<SaveStageData>();
                if (saveData != null)
                {
                    saveData.typeThis = GetRandomStageType();
                }

                floorList.Add(stage);
            }

            Stages.Add(floorList);
        }
        if (Stages.Count > 0)
        {
            var lastFloor = Stages[Stages.Count - 1];
            if (lastFloor.Count > 0)
            {
                var lastStage = lastFloor[0];
                var saveData = lastStage.GetComponent<SaveStageData>();
                if (saveData != null)
                {
                    saveData.typeThis = MapType.Boss;
                }
            }
        }
        for (int floorIndex = 0; floorIndex < TotalFloors - 1; floorIndex++)
        {
            var parents  = Stages[floorIndex];
            var children = Stages[floorIndex + 1];

            int parentCount = parents.Count;
            int childCount  = children.Count;

            for (int pi = 0; pi < parentCount; pi++)
            {
                int leftIndex;
                int rightIndex;

                if (childCount > parentCount)
                {
                    leftIndex  = Mathf.Clamp(pi,     0, childCount - 1);
                    rightIndex = Mathf.Clamp(pi + 1, 0, childCount - 1);
                }
                else
                {
                    float t = (parentCount == 1) ? 0f : (float)pi / (parentCount - 1);
                    float childPos = (childCount == 1) ? 0f : t * (childCount - 1);

                    leftIndex  = Mathf.FloorToInt(childPos);
                    rightIndex = Mathf.CeilToInt(childPos);

                    leftIndex  = Mathf.Clamp(leftIndex,  0, childCount - 1);
                    rightIndex = Mathf.Clamp(rightIndex, 0, childCount - 1);
                }

                GameObject parentStage     = parents[pi];
                GameObject leftChildStage  = children[leftIndex];
                GameObject rightChildStage = children[rightIndex];

                CreateConnectionLine(parentStage, leftChildStage, MapDir.Left);

                CreateConnectionLine(parentStage, rightChildStage, MapDir.Right);
            }
        }
    }

    private void CreateConnectionLine(GameObject parentStage, GameObject childStage, MapDir dir)
    {
        if (LinePrefab == null) return;

        Vector3 startPos = parentStage.transform.position;
        GameObject lineObj = Instantiate(LinePrefab, startPos, Quaternion.identity, parentStage.transform);

        var line = lineObj.GetComponent<LineSetting>();
        if (line != null)
        {
            line.EndPoint = childStage;
            line.Dir      = dir;
            line.CameraObj = cameraObj;
            line.CreateLine();
        }
    }
    private MapType GetRandomStageType()
    {
        int total = PercentByNormalStage + PercentByShopStage + PercentByEventStage + PercentByChoiceStage;
        if (total <= 0)
            return MapType.Battle;

        int r = Random.Range(0, total);

        if (r < PercentByNormalStage)
            return MapType.Battle;

        r -= PercentByNormalStage;
        if (r < PercentByShopStage)
            return MapType.Store;

        r -= PercentByShopStage;
        if (r < PercentByEventStage)
            return MapType.Event;
        return MapType.MapChoice;
    }
}