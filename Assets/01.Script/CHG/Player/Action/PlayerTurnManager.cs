using System;
using System.Collections;
using Assets._01.Script.CHG;
using UnityEngine;


public class PlayerTurnManager : MonoBehaviour
{
    private Player _player;
    public void Init(Player player)
    {
        _player = player;
        AttachPerformer();
        Debug.Log("Player 구독");
    }

    private void AttachPerformer()
    {
        ActionSystem.AttachPerformer<PlayerTurnGA>(EnemyAttack);
    }

    private IEnumerator EnemyAttack(PlayerTurnGA playerTurnGA)
    {
        Debug.Assert(_player.PlayerTarget != null, "PlayerTarget is Null");

        Debug.Log("Player Turn");
        _player.PlayerTarget.HealthCompo.TakeDamage(10);

        yield return new WaitForEndOfFrame();

        //EnemyTurnGA enemyTurnGA = new();
        //ActionSystem.Instance.Perform(enemyTurnGA);
        C_StageManager.Instance.EnemyTurnSet();
    }
}
