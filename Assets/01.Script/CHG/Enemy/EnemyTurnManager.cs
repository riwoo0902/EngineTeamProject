using System.Collections;
using UnityEngine;

public class EnemyTurnManager : MonoBehaviour
{
    private BattleEnemyManager _enemyManger;
    private Player _player;
    private BattleTurnManager _turnManager;

    public void Init(BattleEnemyManager enemyManager, BattleTurnManager turnManager)
    {
        _enemyManger = enemyManager;
        _player = _enemyManger.Player;
        _turnManager = turnManager;

        AttachPerformer();
    }

    private void AttachPerformer()
    {
        ActionSystem.AttachPerformer<EnemyAttackGA>(EnemyAttack);
        ActionSystem.AttachPerformer<EnemyMoveGA>(SlotCheck);
    }

    // 🚨 [필수]: 씬 전환 시 구독 중첩을 방지하기 위해 OnDestroy에서 해지합니다.
    private void OnDestroy()
    {
        OnDeatachPerFormer();
    }

    private void OnDeatachPerFormer()
    {
        ActionSystem.DetachPerFormer<EnemyMoveGA>();
        ActionSystem.DetachPerFormer<EnemyAttackGA>();
    }


    private IEnumerator SlotCheck(EnemyMoveGA enemyMoveGA)
    {
        bool attack = false;

        // 1. 🚶‍♂️ 살아있는 적들의 1차 이동 처리 (순차 이동)
        // 뒤쪽 슬롯부터 순회하며 이동 기회를 확인합니다.
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
                // 앞 칸이 비어 있으면 이동
                int next = cur + 1;
                if (next < _enemyManger.EnemySlots.Count && _enemyManger.EnemySlots[next].CurUse == null)
                {
                    // EnemyMoveWithWait를 호출하여 이동이 끝날 때까지 대기 (순차 이동 보장)
                    yield return EnemyMove(_enemyManger.EnemySlots[cur].CurUse, cur, next);
                }
            }
        }

        // 2. 🔄 1차 이동 완료 후, 죽은 적 재생성 및 재배치 처리
        // BattleEnemyManager에게 재생성 대기열을 처리하도록 요청하고 완료를 기다립니다.
        yield return _enemyManger.HandleReplacementsRoutine();

        // 3. 턴 종료
        if (!attack)
        {
            _turnManager.PlayerTurnSet();
        }
    }

    // 💡 [수정]: Enemy.EnemyMoveWithWait를 사용하여 이동을 대기하고 슬롯 정보를 업데이트합니다.
    private IEnumerator EnemyMove(Enemy enemy, int cur, int next)
    {
        EnemySlot curSlot = _enemyManger.EnemySlots[cur];
        EnemySlot nextSlot = _enemyManger.EnemySlots[next];

        // 1. Enemy 스크립트의 이동 코루틴 실행 및 완료 대기
        yield return enemy.EnemyMove(nextSlot);

        // 2. 이동 완료 후 슬롯 업데이트
        _enemyManger.EnemySlots[next].CurUse = enemy;
        _enemyManger.EnemySlots[cur].CurUse = null;
    }

    // 공격 실행 (액션 시스템의 퍼포머로 등록됨)
    private IEnumerator EnemyAttack(EnemyAttackGA enemyAttackGA)
    {
        _player.TakeDamage(enemyAttackGA.AttackEnemy.Power);
        _player.AgentAnimatorCompo.HurtPlay();
        yield return new WaitForSeconds(1f);
        // 턴 종료는 SlotCheck에서 재생성 로직 완료 후 처리됩니다.
    }
}