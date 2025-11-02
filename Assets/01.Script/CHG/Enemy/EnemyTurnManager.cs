using System.Collections;
using Assets._01.Script.CHG;
using DG.Tweening;
using UnityEngine;

public class EnemyTurnManager : MonoBehaviour
{
    private StageEnemyManager _enemyManger;
    private Player _player;
    //공격 대상
    public void Init(StageEnemyManager enemyManager)
    {
        _enemyManger = enemyManager;

        _player = _enemyManger.Player;


        SubscribeReaction();
        AttachPerformer();
    }

    private void SubscribeReaction() //사전 구독
    {
        ActionSystem.SubscribeReaction<EnemyMoveGA>(StartEnemyTurn, ReactionTiming.PRE); //움직이기 전 턴 바꾸기
    }

    private void AttachPerformer() //행동 등록
    {
        ActionSystem.AttachPerformer<EnemyMoveGA>(SlotCheck);

        ActionSystem.AttachPerformer<EnemyAttackGA>(EnemyAttack);

    }

    //턴 시작 시 사전에 현재 턴 바꿔놓기
    private void StartEnemyTurn(EnemyMoveGA enemyMoveGA)
    {
        C_StageManager.Instance.EnemyTurnSet();
    }

    private IEnumerator SlotCheck(EnemyMoveGA enemyMoveGA)
    {
        bool attack = false;
        Debug.Log("SlotCheck 실행");
        //Dirtionary에서 돌면서 적 발견 -> 적 앞에 칸이 있는가? -> 있으면 이동, 없으면 그대로

        //마지막 칸이라면 Enemy 공격, 아니라면 Enemy 이동
        for (int cur = _enemyManger.EnemySlots.Count - 1; cur >= 0; cur--)
        {
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
                    Debug.Log($"{_enemyManger.EnemySlots[cur].CurUse}, {cur}, {next}");
                    yield return EnemyMove(_enemyManger.EnemySlots[cur].CurUse, cur, next);
                }

            }
        }

        if (!attack) //Enemy들이 공격을 하지 않았을 경우 플레이어 턴으로 전환
            C_StageManager.Instance.PlayerTurnSet();
    }

    //                        현재 칸의 Enemy, 현재 칸 번호, 다음 칸 번호
    private IEnumerator EnemyMove(C_Enemy enemy, int cur, int next)
    {
        bool endMove = false;
        Debug.Log("Enemy Move 실행");

        EnemySlot curSlot = _enemyManger.EnemySlots[cur];
        EnemySlot nextSlot = _enemyManger.EnemySlots[next];

        Debug.Log($"Enemy Pos: {enemy.gameObject.transform.position}, next Pos: {nextSlot.Pos.position}");
        //이동 이후 True로 만들어 진행
        enemy.gameObject.transform.DOMove(nextSlot.Pos.position, 0.3f)
            .OnComplete(() => endMove = true);
        Debug.Log("Move 실행됨");

        yield return new WaitUntil(() => endMove); //Move가 끝나면 실행


        //Slot 바꾸기
        _enemyManger.EnemySlots[next].CurUse = enemy;
        _enemyManger.EnemySlots[cur].CurUse = null;
    }

    //공격 실행 임시
    private IEnumerator EnemyAttack(EnemyAttackGA enemyAttackGA)
    {
        Debug.Log($"Attack Enemy: {enemyAttackGA.AttackEnemy.name}");
        _player.HealthCompo.TakeDamage(10);
        yield return new WaitForSeconds(1f);
        Debug.Log("End Enemy Turn");
        C_StageManager.Instance.PlayerTurnSet();
    }
}
