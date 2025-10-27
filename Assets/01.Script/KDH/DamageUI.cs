using UnityEngine;
using TMPro;
using System;
using UnityEngine.Events;

public class DamageUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI hudDamageText; // 프리팹 연결

    private void Start()
    {
        HealthSystem.OnDeal += TakeDamageUI;
    }

    public void TakeDamageUI(DamageData damageData, Transform targetTransform)
    {
        TextMeshProUGUI hudTextObj = Instantiate(hudDamageText);
        HudDamageText hud = hudTextObj.GetComponent<HudDamageText>();

        // 데미지 값 설정
        hud.damage = damageData.amount.ToString();

        // 위치 설정
        hudTextObj.transform.position = new Vector3(targetTransform.position.x, targetTransform.position.y + 0.5f);

        // 텍스트 색상 변경
        switch (damageData.type)
        {
            case DamageTypeEnum.AD:
                hud.text.color = Color.red;
                break;

            case DamageTypeEnum.AP:
                hud.text.color = Color.blue;
                break;
        }
    }
}