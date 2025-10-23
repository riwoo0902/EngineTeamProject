using UnityEngine;

public class TestAttackSystem : MonoBehaviour
{
    [Header("공격 대상")]
    public HealthSystem target;

    [Header("공격 이펙트")]
    public GameObject adEffectPrefab;  // AD 공격용 이펙트
    public GameObject apEffectPrefab;  // AP 공격용 이펙트

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Attack(DamageTypeEnum.AD, 30);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Attack(DamageTypeEnum.AP, 25);
        }
    }

    private void Attack(DamageTypeEnum type, float amount)
    {
        if (target == null) return;

        // 1. 데미지 생성 후 전달
        DamageData damage = new DamageData(amount, type);
        target.Deal(damage);

        // 2. 타입별 이펙트 재생
        GameObject effectPrefab = null;
        switch (type)
        {
            case DamageTypeEnum.AD:
                effectPrefab = adEffectPrefab;
                break;
            case DamageTypeEnum.AP:
                effectPrefab = apEffectPrefab;
                break;
        }

        if (effectPrefab != null)
        {
            GameObject effect = Instantiate(effectPrefab, target.transform.position, Quaternion.identity);
            effect.GetComponent<ParticleSystem>()?.Play();
            Destroy(effect, 2f);
        }
    }
}