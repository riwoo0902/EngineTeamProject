using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class HealthSystem : MonoBehaviour
{
    public float currentHealth {  get; private set; }

    [SerializeField] private CharacterData characterData;

    [SerializeField] private ParticleSystem particleAp;
    [SerializeField] private ParticleSystem particleAd;


    private void Start()
    {
        particleAd.gameObject.SetActive(false);
        particleAp.gameObject.SetActive(false);
        currentHealth = characterData.maxHealth;
    }

    public void Deal(DamageData damage)
    {
        float finalDamage = damage.amount;

        // 공격 타입이 AD라면 AD 방어력으로 나눔
        if (damage.type == DamageTypeEnum.AD)
        {
            finalDamage /= characterData.adDefense;
        }
        // 공격 타입이 AP라면 AP 방어력으로 나눔
        else if (damage.type == DamageTypeEnum.AP)
        {
            finalDamage /= characterData.apDefense;
        }

        // 체력 차감
        currentHealth -= finalDamage;
        currentHealth = Mathf.Clamp(currentHealth, 0, characterData.maxHealth);

        Debug.Log($"{damage.type} 공격으로 {finalDamage} 피해를 받음 (남은 체력: {currentHealth})");
    }


    public void Heal(int amount)
    {
        currentHealth += amount;
        if (currentHealth > characterData.maxHealth) currentHealth = characterData.maxHealth;
        Mathf.Clamp(currentHealth, 0, characterData.maxHealth);
    }
}
