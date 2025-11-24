using System.Collections;
using UnityEngine;


public class PlayerTurnManager : MonoBehaviour
{

    private Player _player;
    private EnemyTargeting _enemyTargeting;
    private BattleTurnManager _turnManager;
    public void Init(Player player, EnemyTargeting enemyTargeting, BattleTurnManager turnManager)
    {
        Deatach();
        _player = player;
        _enemyTargeting = enemyTargeting;
        _turnManager = turnManager;
        AttachPerformer();
        SubscribeReaction();
    }

    private void AttachPerformer()
    {
        ActionSystem.AttachPerformer<PlayerTurnGA>(PlayerAttack);

    }

    private void SubscribeReaction()
    {
        ActionSystem.SubscribeReaction<PlayerTurnGA>(PlayerTurnEnd, ReactionTiming.POST); //공격 이후 세팅

    }

    private IEnumerator PlayerAttack(PlayerTurnGA playerTurnGA)
    {
        Debug.Assert(_player.PlayerTarget != null, "PlayerTarget is Null");

        if (_player.PlayerTarget == null) yield break;

        //데미지 계산
        //int damage = 
        Debug.Log($"공격 AttackDamage = {_player.AttackDamage}");
        _player.PlayerTarget.HealthCompo.TakeDamage(_player.AttackDamage);

        _player.SetAttackDamage(0);

        yield return new WaitForSeconds(0.5f);


    }

    private void PlayerTurnEnd(PlayerTurnGA playerTurnGA)
    {
        Debug.Log("PlayerTUrnEnd");
        _enemyTargeting.TargetClear();
        _turnManager.EnemyTurnSet();

        EnemyMoveGA enemyMoveGA = new();
        ActionSystem.Instance.AddReaction(enemyMoveGA);
    }

    private void OnDestroy()
    {
        Deatach();
    }

    private void OnDisable()
    {
        Deatach();
    }

    private void Deatach()
    {
        ActionSystem.ClearSubscribers<PlayerTurnGA>(ReactionTiming.POST);
        //ActionSystem.UnsubscribeReaction<PlayerTurnGA>(PlayerTurnEnd, ReactionTiming.POST);
        ActionSystem.DetachPerFormer<PlayerTurnGA>();
    }
}
