using UnityEngine;
using TMPro;

public class DamageUI : MonoBehaviour
{
    [SerializeField] private GameObject hudDamageText; // 프리팹 연결
    private Transform playerTransform;

    private void Start()
    {
        playerTransform.Find("player").GetComponent<Transform>();
    }

    public void TakeDamage(DamageData damageData)
    {
        GameObject hudTextObj = Instantiate(hudDamageText);
        HudDamageText hud = hudTextObj.GetComponent<HudDamageText>();

        // 데미지 값 설정
        hud.damage = damageData.amount.ToString();

        // 위치 설정
        hudTextObj.transform.position = new Vector3(playerTransform.position.x, playerTransform.position.y + 0.5f);

        // 텍스트 색상 변경
        switch (damageData.type)
        {
            case DamageTypeEnum.AD:
                hud.text.color = Color.red;
                break;

            case DamageTypeEnum.AP:
                hud.text.color = Color.green;
                break;
        }
    }
}