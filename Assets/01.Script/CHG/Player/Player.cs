using UnityEngine;

public class Player : Agent
{
    
    public C_Enemy PlayerTarget; 



    protected override void Awake()
    {
        base.Awake();
        
    }
    public void Init(PlayerManager playerManager)
    {
        HealthCompo.Init(playerManager.MaxHealth);


    }
    
    public void ChangeTarget(C_Enemy enemy)
    {
        PlayerTarget = enemy;
    }

    public void StartTurn()
    {

    }

}
