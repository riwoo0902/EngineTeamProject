using UnityEngine;

// 기본 특수효과 SO
[CreateAssetMenu(fileName = "NewMonsterEffect", menuName = "Scriptable Object/Monster Effect", order = int.MaxValue)]
public abstract class MonsterSpecialEffectSO : ScriptableObject
{
    public string effectName;

    public abstract void ApplyEffect(GameObject target);
}

[CreateAssetMenu(fileName = "PoisonEffect", menuName = "Scriptable Object/Monster Effect/Poison", order = int.MaxValue)]
public class PoisonEffectSO : MonsterSpecialEffectSO
{
    public int damagePerSecond;
    public float duration;

    public override void ApplyEffect(GameObject target)
    {
        Debug.Log($"{target.name}가 {damagePerSecond} 데미지를 {duration}초간 받음");
        // 여기서 실제 데미지 로직 구현
    }
}

[CreateAssetMenu(fileName = "StunEffect", menuName = "Scriptable Object/Monster Effect/Stun", order = int.MaxValue)]
public class StunEffectSO : MonsterSpecialEffectSO
{
    public float stunDuration;

    public override void ApplyEffect(GameObject target)
    {
        Debug.Log($"{target.name}가 {stunDuration}초 동안 스턴 상태");
        // 여기서 실제 스턴 로직 구현
    }
}