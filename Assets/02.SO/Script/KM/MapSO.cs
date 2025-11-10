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
    public Sprite Sprite; //어떤 스테이지인지 표시할 때 스프라이트
    public StageType StageType; // 스테이지 유형
    public BattleStageDataSO ThisStageData; //스테이지 레벨 리스트-> 스테이지 Enemy리스트

}
