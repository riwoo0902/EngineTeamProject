using UnityEngine;

public class TestAttackSystem : MonoBehaviour
{
    [Header("공격 대상")]
    public HealthSystem target;

    [Header("공격 이펙트")]
    public GameObject adEffectPrefab;  // AD 공격용 이펙트
    public GameObject apEffectPrefab;  // AP 공격용 이펙트

    GameObject effectPrefab;

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

    public void Attack(DamageTypeEnum type, float amount)
    {
        if (target == null) return;

        DamageData damage = new DamageData(amount, type); // 데미지 생성 후 전달 
        target.Deal(damage);

        switch (type) // 타입에 맞춰 파티클 재생
        {
            case DamageTypeEnum.AD:
                effectPrefab = adEffectPrefab;
                break;
            case DamageTypeEnum.AP:
                effectPrefab = apEffectPrefab;
                break;
        }
            GameObject effect = Instantiate(effectPrefab, target.transform.position, Quaternion.identity);
            effect.GetComponent<ParticleSystem>()?.Play();
    }
}