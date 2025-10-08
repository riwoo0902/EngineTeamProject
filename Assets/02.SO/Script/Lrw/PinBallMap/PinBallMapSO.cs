using Lrw_CustomReadonly;
using UnityEngine;

namespace Lrw_PinBall
{
    [CreateAssetMenu(fileName = "PinBallMapSO", menuName = "Scriptable Objects/PinBallMapSO")]
    public class PinBallMapSO : ScriptableObject
    {
        public static readonly Vector2Int _pinBallStageScale = new(10, 10);
        [SerializeField, ReadOnly] private Vector2Int PinBallStageScale;

        [Header("\n")]

        [SerializeField, Multiline] private string MapData;


        [ContextMenu("CreatFile")]
        private void CreatFile()
        {
            if (CheckMapdata())
            {
                PinBallStageFileManager.instance.CreateJsonFile(MapData, out string createFilePath);
            }
            else
            {
                Debug.LogError("Can not CreateFile");
            }
        }



        private void OnValidate()
        {
            PinBallStageScale = _pinBallStageScale;
            Debug.Log(MapData);
            Debug.Log(MapData.Split('\n').Length);
        }

        private bool CheckMapdata()
        {
            string[] a = MapData.Split("\n");
            if (a.Length == _pinBallStageScale.y) return false;
            foreach (string b in a)
            {
                if(b.Length == _pinBallStageScale.x) return false;
            }

            return true;
        }


    }
}

