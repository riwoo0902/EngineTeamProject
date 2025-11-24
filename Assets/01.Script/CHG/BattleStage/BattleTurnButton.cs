using System;
using _01.Script.Lrw.Manager;
using _01.Script.Lrw.PinBallCompo.FSM;
using _01.Script.Lrw.PinBallMap;
using Custom.MonoSingleton;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BattleTurnButton : MonoSingleton<BattleTurnButton>
{
    private BattleTurnManager _turnManager;
    private BattleStageContect _contect;
    private Button _button;
    [SerializeField] private TextMeshProUGUI _textMeshProUGUI;
    public bool ShowText { get; set; }  = false;

    public void Init(BattleStageContect contect)
    {
        _contect = contect;
        _turnManager = contect.TurnManager;
    }
    
    protected override void Awake()
    {
        base.Awake();
        _button = GetComponent<Button>();
    }

    private void PinBallSpawnFalse()
    {
        PinBallSpawner.Instance.SetCanSpawn(false);
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
        PinBallSpawnFalse();
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
        if (_contect.Player.PlayerTarget == null)
        {
            _textMeshProUGUI.color = new Color(_textMeshProUGUI.color.r,_textMeshProUGUI.color.g,_textMeshProUGUI.color.b,1f);
        }
        else
        {
            _textMeshProUGUI.color = new Color(_textMeshProUGUI.color.r, _textMeshProUGUI.color.g, _textMeshProUGUI.color.b, 0f);
        }
        
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
}
