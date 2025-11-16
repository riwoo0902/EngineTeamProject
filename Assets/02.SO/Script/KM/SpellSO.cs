using System.Collections.Generic;
using UnityEngine;

public enum SpellType
{
    UsePinBall,
    UsePlayer
}


[CreateAssetMenu(fileName = "SpellSO", menuName = "SO/KM/SpellSO")]
public class SpellSO : ScriptableObject
{
    public SpellType ThisSpellType;
    public List<SpellDataSO> ThisSpellDataList;
}
