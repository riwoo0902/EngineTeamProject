using System.Collections;
using UnityEngine;

public class EnemyTurnManager : MonoBehaviour
{
    private BattleEnemyManager _enemyManger;
    private Player _player;
    private BattleTurnManager _turnManager;
    //공격 대상
    public void Init(BattleEnemyManager enemyManager, BattleTurnManager turnManager)
    {
        _enemyManger = enemyManager;
        _player = _enemyManger.Player;
        _turnManager = turnManager;


        AttachPerformer();

    }

    private void AttachPerformer() //행동 등록
    {
        ActionSystem.AttachPerformer<EnemyMoveGA>(SlotCheck);

        ActionSystem.AttachPerformer<EnemyAttackGA>(EnemyAttack);

    }


    private IEnumerator SlotCheck(EnemyMoveGA enemyMoveGA)
    {
        bool attack = false;
        //Dirtionary에서 돌면서 적 발견 -> 적 앞에 칸이 있는지 확인 -> 있으면 이동, 없으면 그대로

        //마지막 칸이라면 Enemy 공격, 아니라면 Enemy 이동
        for (int cur = _enemyManger.EnemySlots.Count - 1; cur >= 0; cur--)
        {

            Debug.Log("SlotCheck 실행");

            EnemySlot slot = _enemyManger.EnemySlots[cur];
            if (slot.CurUse == null) continue; //자리에 Enemy가 없다면 다시

            //마지막칸일 경우 공격
            if (cur == _enemyManger.EnemySlots.Count - 1)
            {
                EnemyAttackGA enemyAttackGA = new(_enemyManger.EnemySlots[_enemyManger.EnemySlots.Count - 1].CurUse);
                ActionSystem.Instance.AddReaction(enemyAttackGA);
                attack = true;
                continue;
            }
            else //아닐경우 이동
            {
                //앞자리가 있고 앞자리에 Enemy가 없다면 이동
                int next = cur + 1;
                if (next < _enemyManger.EnemySlots.Count && _enemyManger.EnemySlots[next].CurUse == null)
                {
                    yield return EnemyMove(_enemyManger.EnemySlots[cur].CurUse, cur, next);
                }

            }
        }

        if (!attack) //Enemy들이 공격을 하지 않았을 경우 플레이어 턴으로 전환
            _turnManager.PlayerTurnSet();
    }

    //                        현재 칸의 Enemy, 현재 칸 번호, 다음 칸 번호
    private IEnumerator EnemyMove(Enemy enemy, int cur, int next)
    {
        Debug.Log("Move실행");

        bool endMove = false;
        EnemySlot curSlot = _enemyManger.EnemySlots[cur];
        EnemySlot nextSlot = _enemyManger.EnemySlots[next];

        //이동 이후 True로 만들어 진행
        enemy.EnemyMove(nextSlot, () =>
        {
            //Slot 바꾸기
            _enemyManger.EnemySlots[next].CurUse = enemy;
            _enemyManger.EnemySlots[cur].CurUse = null;
            endMove = true;
        });

        yield return new WaitUntil(() => endMove);

    }



    //공격 실행 임시
    private IEnumerator EnemyAttack(EnemyAttackGA enemyAttackGA)
    {
        _player.TakeDamage(enemyAttackGA.AttackEnemy.Power);
        _player.AgentAnimatorCompo.HurtPlay();
        yield return new WaitForSeconds(1f); 
        //_turnManager.PlayerTurnSet();
    }

    private void OnDestroy()
    {
        ActionSystem.DetachPerFormer<EnemyMoveGA>();
        ActionSystem.DetachPerFormer<EnemyAttackGA>();

    }
}
