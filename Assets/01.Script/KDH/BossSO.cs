using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BossSO", menuName = "SO/BossSO")]
public class BossSO : ScriptableObject
{
    [Header("basic abilities")]
    public string bossName;
    public float maxHP = 100f;
    public int patternDelay = 2;

    [Header("Pattern settings")]
    public float attackCoolTime = 2f;     
    public float skillCoolTime = 5f;  
    public List<GameObject> attackPrefabs; // normal attack
    public List<GameObject> skillPrefabs;  // skill Attack

    [Header("Phase transition conditions")]
    public float phase2HP = 0.5f;       // Phase transition stamina

}
