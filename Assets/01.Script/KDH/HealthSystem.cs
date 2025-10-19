using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class HealthSystem : MonoBehaviour
{
    public float currentHealth {  get; private set; }
    public float maxHealth { get; private set; } = 7;
    public float minHealth { get; private set; }

    private float Ad = 10f;
    private float Ap = 5f;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void Deal(DamageData damage)
    {
        switch (damage.type)
        {
            case DamageTypeEnum.AD:
            currentHealth -= Ad;
            break;

            case DamageTypeEnum.AP:
            currentHealth -= Ap;
            break;
        }

        Debug.Log($"{damage.type} 공격 {damage.amount} 피해, (남은 체력 : {currentHealth})");

    }

    public void Heal(int amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth) currentHealth = maxHealth;
    }
}
