using UnityEngine;
using TMPro;
using System;
using UnityEngine.Events;

public class DamageUI : MonoBehaviour
{
    [SerializeField] private GameObject hudDamageText; // 프리팹
    [SerializeField] private Canvas mainCanvas; // UI 캔버스 연결


    private void Awake()
    {
        if (mainCanvas == null)
        {
            mainCanvas = FindAnyObjectByType<Canvas>();
        }


    }

    private void Start()
    {
        HealthSystem.OnDeal += TakeDamageUI;
    }

    public void TakeDamageUI(DamageData damageData, Transform targetTransform)
    {
        GameObject hudTextObj = Instantiate(hudDamageText, mainCanvas.transform);

        HudDamageText hud = hudTextObj.GetComponent<HudDamageText>();
        TextMeshProUGUI tmp = hudTextObj.GetComponentInChildren<TextMeshProUGUI>(true);

        hud.SetDamage(damageData.amount);

        hudTextObj.transform.position = Camera.main.WorldToScreenPoint(
            targetTransform.position + Vector3.up * 0.5f
        );

        // 6. 색상 변경
        switch (damageData.type)
        {
            case DamageTypeEnum.AD:
                tmp.color = Color.red;
                break;
            case DamageTypeEnum.AP:
                tmp.color = Color.blue;
                break;
        }
    }
}