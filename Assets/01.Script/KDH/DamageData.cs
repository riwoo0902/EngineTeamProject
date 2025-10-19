using UnityEngine;

[System.Serializable]
public class DamageData
{
    public float amount;
    public DamageTypeEnum type;

    public DamageData (float amount, DamageTypeEnum type)
    {
        this.amount = amount;
        this.type = type;
    }
}
