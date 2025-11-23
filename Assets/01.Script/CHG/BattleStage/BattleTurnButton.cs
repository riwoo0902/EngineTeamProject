using System;
using _01.Script.Lrw.Manager;
using _01.Script.Lrw.PinBallCompo.FSM;
using _01.Script.Lrw.PinBallMap;
using UnityEngine;
using UnityEngine.UI;

public class BattleTurnButton : MonoBehaviour
{
    private BattleTurnManager _turnManager;
    private BattleStageContect _contect;
    private Button _button;
    public void Init(BattleStageContect contect)
    {
        _contect = contect;
        _turnManager = contect.TurnManager;
    }

    private void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(PinBallSpawnFalse);
    }

    private void PinBallSpawnFalse()
    {
        PinBallSpawner.Instance.SetCanSpawn(false
            );
        Debug.Log("pinballSpawnFalse");
    }
    
    public void PlayerTurnButton()
    {
        if (!_turnManager.CurTurn || _contect.Player.PlayerTarget == null) return;
        if(GameManager.Instance.state != PinBallStates.Idle) return;
        //Debug.Log($"CurTurn: {(_turnManager.CurTurn ? "Player" : "Enemy")}");
        //PlayerTurnGA playerTurnGA = new();
        //ActionSystem.Instance.Perform(playerTurnGA);
        _contect.Player.AgentAnimatorCompo.AttackPlay();
    }

    public void EnemyTurnButton()
    {
        if (_turnManager.CurTurn) return;

        Debug.Log($"CurTurn: {(_turnManager.CurTurn ? "Player" : "Enemy")}");
        EnemyMoveGA enemyMoveGA = new();
        ActionSystem.Instance.Perform(enemyMoveGA); //EnemyTurn����

    }

    private void Update()
    {
        try
        {
            if ((!_turnManager.CurTurn || _contect.Player.PlayerTarget == null) ||
                (GameManager.Instance.state != PinBallStates.Idle))
            {
                _button.interactable = false;
                return;
            }
            _button.interactable = true;
        }
        catch
        {
            _button.interactable = false;
        }
        
        
    }

    private void OnDestroy()
    {
        _button.onClick.RemoveAllListeners();
    }
}
