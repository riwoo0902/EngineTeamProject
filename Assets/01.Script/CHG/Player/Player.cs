using UnityEngine;

public class Player : Agent
{
    [HideInInspector]
    public C_Enemy PlayerTarget; 



    protected override void Awake()
    {
        
    }
    public void Init(PlayerManager playerManager)
    {
        base.Awake();
        Debug.Assert(playerManager != null, "PlayerManager is Null");
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
