using UnityEngine;

public class AttackSystem : MonoBehaviour
{
    public HealthSystem target; // 인스펙터에서 적 연결

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Debug.Log("AD 공격");
            DamageData ad = new DamageData(30, DamageTypeEnum.AD);
            target.Deal(ad);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Debug.Log("AP 공격");
            DamageData ap = new DamageData(25, DamageTypeEnum.AP);
            target.Deal(ap);
        }
    }
}