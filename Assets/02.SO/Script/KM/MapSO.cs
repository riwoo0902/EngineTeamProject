using UnityEngine;

public enum StageType
{
    Normal,
    Boss,
    MiniBoss,
    Bonus
}

[CreateAssetMenu(fileName = "MapSO", menuName = "SO/KM/MapSO")]
public class MapSO : ScriptableObject
{
    public StageType Stage;
    public C_StageDataSO ThisStageData;
}
