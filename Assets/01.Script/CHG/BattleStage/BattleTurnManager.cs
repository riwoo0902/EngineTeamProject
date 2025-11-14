using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BattleTurnManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI TurnText;
    [SerializeField] private Transform MovePos;
    private Transform CurPos => transform;
    public bool CurTurn { get; private set; } = true; //true일시 플레이어 턴
    public int TurnCount { get; private set; } = 1;

    public void EnemyTurnSet()
    {
        CurTurn = false;
    }
    public void PlayerTurnSet()
    {
        CurTurn = true;
        TurnText.text = $"Turn {TurnCount}";
        TurnCount += 1;
        TurnText.transform.DOMove(MovePos.transform.position, 1f).SetEase(Ease.OutQuint).OnComplete(() =>
        {
            TurnText.transform.DOMove(CurPos.position, 1f).SetEase(Ease.OutQuint);
        });
    }



}
