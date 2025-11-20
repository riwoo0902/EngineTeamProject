using UnityEngine;

public class EventReaction : MonoBehaviour
{
    public static EventReaction Instance {  get; private set; }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void RetouchMaxHealth(int n)
    {
        if (n > 0)
            PlayerManager.Instance.AddMaxHealth(n);
        else
            PlayerManager.Instance.SpendMaxHealth(n);
    }

    public void RetouchCurrentHealth(int n)
    {
        if (n > 0)
            PlayerManager.Instance.AddCurrentHealth(n);
        else
            PlayerManager.Instance.SpendCurrentHealth(n);
    }


}
