using System;
using System.Collections;
using UnityEngine;

public class EnemyTurnManager : MonoBehaviour
{
    private BattleEnemyManager _enemyManger;
    private Player _player;
    private BattleTurnManager _turnManager;
    public Action EnemyTurnEnd;
    public void Init(BattleEnemyManager enemyManager, BattleTurnManager turnManager)
    {
        OnDeatachPerFormer();

        _enemyManger = enemyManager;
        _player = _enemyManger.Player;
        _turnManager = turnManager;

        AttachPerformer();
    }

    private void AttachPerformer()
    {
        Debug.Log("Enemy구독");
        ActionSystem.AttachPerformer<EnemyAttackGA>(EnemyAttack);
        ActionSystem.AttachPerformer<EnemyMoveGA>(SlotCheck);
    }

    private void OnDestroy()
    {
        OnDeatachPerFormer();
    }
    private void OnDisable()
    {
        // OnDestroy가 호출되기 전/후의 파괴 과정에서 문제가 있을 경우를 대비
        OnDeatachPerFormer();
    }
    private void OnDeatachPerFormer()
    {
        Debug.Log("Enemy구독 해제");
        ActionSystem.DetachPerFormer<EnemyMoveGA>();
        ActionSystem.DetachPerFormer<EnemyAttackGA>();
    }


    private IEnumerator SlotCheck(EnemyMoveGA enemyMoveGA)
    {
        bool attack = false;
        for (int cur = _enemyManger.EnemySlots.Count - 1; cur >= 0; cur--)
        {
            EnemySlot slot = _enemyManger.EnemySlots[cur];
            if (slot.CurUse == null) continue;

            // 가장 앞 칸일 경우 공격
            if (cur == _enemyManger.EnemySlots.Count - 1)
            {
                EnemyAttackGA enemyAttackGA = new(_enemyManger.EnemySlots[_enemyManger.EnemySlots.Count - 1].CurUse);
                ActionSystem.Instance.AddReaction(enemyAttackGA);
                attack = true;
                continue;
            }
            else
            {
                int next = cur + 1;
                if (next < _enemyManger.EnemySlots.Count && _enemyManger.EnemySlots[next].CurUse == null)
                {

                    yield return EnemyMove(_enemyManger.EnemySlots[cur].CurUse, cur, next);
                }
            }
        }

        yield return _enemyManger.HandleReplacementsRoutine();
        
        EnemyTurnEnd?.Invoke();

        if (!attack)
        {
            _turnManager.PlayerTurnSet();
        }
    }

    private IEnumerator EnemyMove(Enemy enemy, int cur, int next)
    {
        EnemySlot curSlot = _enemyManger.EnemySlots[cur];
        EnemySlot nextSlot = _enemyManger.EnemySlots[next];

        yield return enemy.EnemyMove(nextSlot);


        _enemyManger.EnemySlots[next].CurUse = enemy;
        _enemyManger.EnemySlots[cur].CurUse = null;
    }


    private IEnumerator EnemyAttack(EnemyAttackGA enemyAttackGA)
    {
        _player.TakeDamage(enemyAttackGA.AttackEnemy.Power);
        _player.AgentAnimatorCompo.HurtPlay();
        yield return new WaitForSeconds(1f);

    }
}