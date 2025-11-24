using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyTargeting : MonoBehaviour
{
    [SerializeField]
    private LayerMask monsterLayer;
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
            Collider2D[] rayHit = Physics2D.OverlapPointAll(pos);
            
            bool flag = false;
            
            if (rayHit.Length > 0)
            {
                rayHit.ToList().ForEach((c) =>
                {
                    if (c.TryGetComponent<Enemy>(out Enemy enemy))
                    {
                        TargetSet(enemy);
                        flag = true;
                    }
                });
                
                if(!flag)
                    TargetClear();
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
        _contect.UIManager.TargetingImgShow(enemy.transform);

        _player.ChangeTarget(enemy);
    }
}
