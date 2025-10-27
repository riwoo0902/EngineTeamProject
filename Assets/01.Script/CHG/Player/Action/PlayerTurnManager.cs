using System;
using System.Collections;
using UnityEngine;


public class PlayerTurnManager : MonoBehaviour
{
    private Player _player;
    public void Init(Player player)
    {
        _player = player;
        AttachPerformer();
        SubscribeReaction();
    }

    private void AttachPerformer()
    {
        ActionSystem.AttachPerformer<PlayerTurnGA>(EnemyAttack);
    }

    private void SubscribeReaction()
    {

    }

    private IEnumerator EnemyAttack(PlayerTurnGA playerTurnGA)
    {
        _player.PlayerTarget.HealthCompo.TakeDamage(10);
        yield return new WaitForSeconds(1f);
        EnemyTurnGA enemyTurnGA = new();
        ActionSystem.Instance.Perform(enemyTurnGA);
    }
}
