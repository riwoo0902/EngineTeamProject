using System;
using UnityEngine;

public enum PinBallSpellType
{
    a,
    b
}

public enum PlayerSpellType
{
    c,
    d
}

[CreateAssetMenu(fileName = "SpellDataSO", menuName = "SO/KM/SpellData/SpellDataSO")]
public class SpellDataSO : ScriptableObject
{
    public string SpellName;
    public Sprite SpellIcon;
    public string SpellDescription;
    public Action SpellUse;
}
