using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class HealthSystem : MonoBehaviour
{
    public float currentHealth { get; private set; }

    [SerializeField] private CharacterData characterData;

    [SerializeField] private ParticleSystem particleAp;
    [SerializeField] private ParticleSystem particleAd;


    private void Start()
    {
        currentHealth = characterData.maxHealth;
    }

    public void Deal(DamageData damage)
    {
        float finalDamage = damage.amount;

        if (damage.type == DamageTypeEnum.AD)
        {
            finalDamage -= characterData.adDefense;
            if (finalDamage < 0)
            {
                finalDamage = 0;
            }
        }

        else if (damage.type == DamageTypeEnum.AP)
        {
            finalDamage -= characterData.apDefense;
            if (finalDamage < 0)
            {
                finalDamage = 0;
            }
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
