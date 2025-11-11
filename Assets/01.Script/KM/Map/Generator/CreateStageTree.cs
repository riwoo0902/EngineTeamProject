using System.Collections.Generic;
using UnityEngine;

public class CreateStageTree : MonoBehaviour
{
    [SerializeField] private int percent = 10;
    [SerializeField] private int countZeropercent = 3;
    [SerializeField] private GameObject StagePrefab;
    [SerializeField] private GameObject LinePrefab;
    [SerializeField] private GameObject Parent;

    private int _currentPercent = 0;
    private int _currentFloor = 0;

    [SerializeField] private float horizontalSpacing = 3f;
    [SerializeField] private float verticalSpacing = 3f;
    [SerializeField] private Vector3 origin = Vector3.zero;

    [ContextMenu("CreateStages")]
    private void CreateStages()
    {
        List<List<GameObject>> stageParents = new List<List<GameObject>>();
        List<List<GameObject>> StageLineRends = new List<List<GameObject>>();

        int rand = Random.Range(0, 100);
        while (_currentPercent < rand)
        {
            _currentPercent += percent;
            _currentFloor++;

            var thisFloorParents = new List<GameObject>();
            var thisFloorLines = new List<GameObject>();

            float startX = -((_currentFloor - 1) * 0.5f) * horizontalSpacing;

            for (int i = 0; i < _currentFloor; i++)
            {
                float x = origin.x + startX + (i * horizontalSpacing);
                float y = origin.y - (_currentFloor * verticalSpacing);
                Vector3 pos = new Vector3(x, y, 0f);

                GameObject stage = Instantiate(StagePrefab, pos, Quaternion.identity, transform);
                GameObject lineObj = Instantiate(LinePrefab, pos, Quaternion.identity, transform);
                stage.transform.parent = Parent.transform;

                thisFloorParents.Add(stage);
                thisFloorLines.Add(lineObj);
            }

            stageParents.Add(thisFloorParents);
            StageLineRends.Add(thisFloorLines);

            if (_currentFloor >= 2)
            {
                List<GameObject> prevLines = StageLineRends[_currentFloor - 2];
                List<GameObject> currStages = stageParents[_currentFloor - 1];

                for (int pi = 0; pi < prevLines.Count; pi++)
                {
                    int left = Mathf.Clamp(pi, 0, currStages.Count - 1);
                    int right = Mathf.Clamp(pi + 1, 0, currStages.Count - 1);

                    var lineL = prevLines[pi].GetComponent<LineSetting>();
                    lineL.transform.parent = Parent.transform;
                    if (lineL != null)
                    {
                        lineL.EndPoint = currStages[left];
                        lineL.CreateLine();
                    }

                    var lineObjR = Instantiate(LinePrefab, prevLines[pi].transform.position, Quaternion.identity, transform);
                    lineObjR.transform.parent = Parent.transform;
                    var lineR = lineObjR.GetComponent<LineSetting>();
                    if (lineR != null)
                    {
                        lineR.EndPoint = currStages[right];
                        lineR.CreateLine();
                    }
                }
            }
        }
        for(int i = 0; i < transform.childCount; i++)
        {
            if(transform.GetChild(i).gameObject != Parent)
            {
                Destroy(transform.GetChild(i).gameObject);
            }
        }
    }
}