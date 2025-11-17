using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyTargeting : MonoBehaviour
{
    private Player _player;
    private BattleUIManager _uIManager;
    public void Init(Player player, BattleUIManager uIManager)
    {
        _player = player;
        _uIManager = uIManager;
    }

    private void Update()
    {
        //if (!C_StageManager.Instance.CurTurn) return;

        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject())
                return;

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
        _uIManager.TargetingImgHide();
        _player.ChangeTarget(null);
    }

    private void TargetSet(Enemy enemy)
    {
        Debug.Log(enemy.EnemyData.EnemyName);
        _uIManager.TargetingImgShow(enemy.transform);

        _player.ChangeTarget(enemy);
    }
}
