using System.Collections;
using Assets._01.Script.CHG;
using Assets._01.Script.CHG.Enemy;
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
        Debug.Log("Enemy 구독");
    }

    private void SubscribeReaction() //사전 구독
    {
        ActionSystem.SubscribeReaction<EnemyTurnGA>(StartEnemyTurn, ReactionTiming.PRE);
    }

    private void AttachPerformer() //행동 등록
    {
        ActionSystem.AttachPerformer<EnemyTurnGA>(PlayerAttack);
    }

    //턴 시작 시 사전에 현재 턴 바꿔놓기
    private void StartEnemyTurn(EnemyTurnGA enemyTurnGA)
    {
        C_StageManager.Instance.EnemyTurnSet();
        Debug.Log($"사전 실행 성공");
    }

    //공격 실행 임시
    private IEnumerator PlayerAttack(EnemyTurnGA enemyTurnGA)
    {
        Debug.Log("Enemy Turn");
        _player.HealthCompo.TakeDamage(10);
        yield return new WaitForSeconds(3f);
        Debug.Log("End Enemy Turn");
        C_StageManager.Instance.PlayerTurnSet();
    }
}
