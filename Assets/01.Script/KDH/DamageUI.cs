using UnityEngine;
using TMPro;
using System;
using UnityEngine.Events;

public class DamageUI : MonoBehaviour
{
    [SerializeField] private GameObject hudDamageText; // ������ ����
    [SerializeField] private Canvas mainCanvas; // UI ĵ���� ����


    private void Awake()
    {
        if (mainCanvas == null)
        {
            mainCanvas = FindAnyObjectByType<Canvas>();
        }


    }

    private void Start()
    {

    }


    public void TakeDamageUI(DamageData damageData, Transform targetTransform)
    {
        // 1. 프리팹 생성
        GameObject hudTextObj = Instantiate(hudDamageText, mainCanvas.transform);

        // 2. HudDamageText 컴포넌트 가져오기
        HudDamageText hud = hudTextObj.GetComponent<HudDamageText>();

        // 3. TMP 컴포넌트 가져오기 (색상용)
        TextMeshProUGUI tmp = hudTextObj.GetComponentInChildren<TextMeshProUGUI>(true);

        // 4. 데미지 값 설정
        hud.SetDamage(damageData.amount);

        // 5. 위치 지정
        hudTextObj.transform.position = new Vector3(targetTransform.position.x, targetTransform.position.y + 0.5f);

        // 6. 색상 변경
        switch (damageData.type)
        {
            case DamageTypeEnum.AD:
                tmp.color = Color.red;
                break;
            case DamageTypeEnum.AP:
                tmp.color = Color.blue;
                break;
            default:
                tmp.color = Color.white;
                break;
        }
    }
}