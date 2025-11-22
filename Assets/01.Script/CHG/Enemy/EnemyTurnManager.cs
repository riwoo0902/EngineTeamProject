using System.Collections;
using UnityEngine;

public class EnemyTurnManager : MonoBehaviour
{
    private BattleEnemyManager _enemyManger;
    private Player _player;
    private BattleTurnManager _turnManager;
    //���� ���
    public void Init(BattleEnemyManager enemyManager, BattleTurnManager turnManager)
    {
        _enemyManger = enemyManager;
        _player = _enemyManger.Player;
        _turnManager = turnManager;


        AttachPerformer();

    }

    private void AttachPerformer() //�ൿ ���
    {
        ActionSystem.AttachPerformer<EnemyAttackGA>(EnemyAttack);
        ActionSystem.AttachPerformer<EnemyMoveGA>(SlotCheck);


    }


    private IEnumerator SlotCheck(EnemyMoveGA enemyMoveGA)
    {
        bool attack = false;
        //Dirtionary���� ���鼭 �� �߰� -> �� �տ� ĭ�� �ִ��� Ȯ�� -> ������ �̵�, ������ �״��

        //������ ĭ�̶�� Enemy ����, �ƴ϶�� Enemy �̵�
        for (int cur = _enemyManger.EnemySlots.Count - 1; cur >= 0; cur--)
        {


            EnemySlot slot = _enemyManger.EnemySlots[cur];
            if (slot.CurUse == null) continue; //�ڸ��� Enemy�� ���ٸ� �ٽ�

            //������ĭ�� ��� ����
            if (cur == _enemyManger.EnemySlots.Count - 1)
            {
                EnemyAttackGA enemyAttackGA = new(_enemyManger.EnemySlots[_enemyManger.EnemySlots.Count - 1].CurUse);
                ActionSystem.Instance.AddReaction(enemyAttackGA);
                attack = true;
                continue;
            }
            else //�ƴҰ�� �̵�
            {
                //���ڸ��� �ְ� ���ڸ��� Enemy�� ���ٸ� �̵�
                int next = cur + 1;
                if (next < _enemyManger.EnemySlots.Count && _enemyManger.EnemySlots[next].CurUse == null)
                {
                    yield return EnemyMove(_enemyManger.EnemySlots[cur].CurUse, cur, next);
                }

            }
        }

        if (!attack) //Enemy���� ������ ���� �ʾ��� ��� �÷��̾� ������ ��ȯ
            _turnManager.PlayerTurnSet();
    }

    //                        ���� ĭ�� Enemy, ���� ĭ ��ȣ, ���� ĭ ��ȣ
    private IEnumerator EnemyMove(Enemy enemy, int cur, int next)
    {

        bool endMove = false;
        EnemySlot curSlot = _enemyManger.EnemySlots[cur];
        EnemySlot nextSlot = _enemyManger.EnemySlots[next];

        //�̵� ���� True�� ����� ����
        enemy.EnemyMove(nextSlot, () =>
        {
            //Slot �ٲٱ�
            _enemyManger.EnemySlots[next].CurUse = enemy;
            _enemyManger.EnemySlots[cur].CurUse = null;
            endMove = true;
        });

        yield return new WaitUntil(() => endMove);

    }



    //���� ���� �ӽ�
    private IEnumerator EnemyAttack(EnemyAttackGA enemyAttackGA)
    {
        _player.TakeDamage(enemyAttackGA.AttackEnemy.Power);
        _player.AgentAnimatorCompo.HurtPlay();
        yield return new WaitForSeconds(1f); 
        //_turnManager.PlayerTurnSet();
    }

    private void OnDeatachPerFormer()
    {
        ActionSystem.DetachPerFormer<EnemyMoveGA>();
        ActionSystem.DetachPerFormer<EnemyAttackGA>();

    }
}
