using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class BattleTurnManager : MonoBehaviour
{
    [SerializeField] private Image TurnImage;
    public bool CurTurn { get; private set; } = true; //true일시 플레이어 턴


    public void EnemyTurnSet()
    {
        CurTurn = false;
        TurnImage.DOColor(Color.red, 0);

    }
    public void PlayerTurnSet()
    {
        CurTurn = true;
        TurnImage.DOColor(Color.blue, 0);
    }
}
