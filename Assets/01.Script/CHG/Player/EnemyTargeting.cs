using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyTargeting : MonoBehaviour
{
    private Player _player;
    private BattleStageContect _contect;
    public void Init(Player player, BattleStageContect contect)
    {
        _player = player;
        _contect = contect;
    }

    private void Update()
    {

        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            if (!_contect.TurnManager.CurTurn) return;

            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D rayHit = Physics2D.Raycast(pos, Vector2.zero, 0f);

            if (rayHit.collider != null && rayHit.collider.TryGetComponent<Enemy>(out Enemy enemy))
            {
                TargetSet(enemy);
            }
            else
            {
                TargetClear();
            }
        }
    }

    public void TargetClear()
    {
        _contect.UIManager.TargetingImgHide();
        _player.ChangeTarget(null);
    }

    private void TargetSet(Enemy enemy)
    {
        Debug.Log(enemy.EnemyData.EnemyName);
        _contect.UIManager.TargetingImgShow(enemy.transform);

        _player.ChangeTarget(enemy);
    }
}
