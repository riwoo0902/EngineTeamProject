using UnityEngine;

public class BattleStageContext : MonoBehaviour
{
    [SerializeField] private EnemyStageManager _enemyManager;
    [SerializeField] private BattleTurnManager _turnManager;
    [SerializeField] private Player _player;
    [SerializeField] private BattleTurnButton _turnButton;
    public void Init(BattleStageDataSO enemyData)
    {
        _enemyManager.Init(enemyData, _player, _turnManager);
        _player.Init(_turnManager);
        _turnButton.Init(_turnManager);

        _turnManager.PlayerTurnSet();

    }


}
