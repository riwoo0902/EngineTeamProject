using System;
using System.Collections;
using UnityEngine;


public class PlayerTurnManager : MonoBehaviour
{

    private Player _player;
    private EnemyTargeting _enemyTargeting;

    public void Init(Player player, EnemyTargeting enemyTargeting)
    {
        _player = player;
        _enemyTargeting = enemyTargeting;
        AttachPerformer();
        Debug.Log("Player 구독");
    }

    private void AttachPerformer()
    {
        ActionSystem.AttachPerformer<PlayerTurnGA>(PlayerAttack);
    }

    private IEnumerator PlayerAttack(PlayerTurnGA playerTurnGA)
    {
        Debug.Assert(_player.PlayerTarget != null, "PlayerTarget is Null");

        if (_player.PlayerTarget == null) yield break;

        Debug.Log("Player Turn");
        _player.PlayerTarget.HealthCompo.TakeDamage(10);

        yield return new WaitForEndOfFrame();

        _enemyTargeting.TargetClear();
        C_StageManager.Instance.EnemyTurnSet();
    }
}
