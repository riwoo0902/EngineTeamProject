using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class HealthSystem : MonoBehaviour
{
    public int currentHealth {  get; private set; }
    public int maxHealth { get; private set; } = 7;
    public int minHealth { get; private set; }

    public event Action OnDamageTaken;
    public event Action OnHeal;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void Deal(int damage)
    {
        currentHealth -= damage;
        if (currentHealth < minHealth) currentHealth = minHealth;
        OnDamageTaken?.Invoke();
    }

    public void Heal(int amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth) currentHealth = maxHealth;
        OnHeal?.Invoke();
    }

    private void Update()
    {
        if (Keyboard.current.aKey.wasPressedThisFrame)
        {
            Deal(1);
        }

        if (Keyboard.current.wKey.wasPressedThisFrame)
        {
            Heal(1);
        }
    }


}
