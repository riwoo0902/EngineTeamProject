using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BossSO", menuName = "SO/BossSO")]
public class BossSO : ScriptableObject
{
    [Header("기본 능력치")]
    public string bossName;
    public float maxHP = 100f;
    public int patternDelay = 2;

    [Header("패턴 설정")]
    public float attackCoolTime = 2f;      // 공격 간격 (패턴 사이 딜레이)
    public float skillCoolTime = 5f;       // 스킬 사용 쿨다운
    public List<GameObject> attackPrefabs; // 일반 공격에 사용하는 프리팹
    public List<GameObject> skillPrefabs;  // 특수 스킬용 프리팹

    [Header("페이즈 전환 조건")]
    public float phase2HP = 0.5f;       // 페이즈 전환 체력

}
