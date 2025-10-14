using UnityEngine;

public class Player : Agent
{
    public C_Enemy PlayerTarget; 

    protected override void Awake()
    {
        base.Awake();
        transform.position = new Vector2(1, 1);
    }
    public void ChangeTarget(C_Enemy enemy)
    {
        PlayerTarget = enemy;
        PlayerTarget.HealthCompo.GetDamage(5);
    }

    

}
