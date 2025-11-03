using Assets._01.Script.CHG;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyTargeting : MonoBehaviour
{
    [SerializeField] private GameObject TargetTriangle; //타겟 표시용, 이름 바꿔야함
    private SpriteRenderer _spren; //이름 바꿔야함
    private Color _sprenColor; //이름 바꿔야함
    private Player _player;

    public void Init(Player player)
    {
        _player = player;
        _spren = TargetTriangle.GetComponent<SpriteRenderer>();
        _sprenColor = _spren.color;
        _sprenColor.a = 0;
        _spren.color = _sprenColor;
    }



    private void Update()
    {
        if (!C_StageManager.Instance.CurTurn) return;

        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject())
                return;

            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D rayHit = Physics2D.Raycast(pos, Vector2.zero, 0f);

            if (rayHit.collider != null && rayHit.collider.TryGetComponent<C_Enemy>(out C_Enemy enemy))
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
        _sprenColor.a = 0;
        _spren.color = _sprenColor;
        _player.ChangeTarget(null);
    }

    private void TargetSet(C_Enemy enemy)
    {
        _sprenColor.a = 1;
        _spren.color = _sprenColor;  
        Vector3 targetPos = enemy.transform.position;
        TargetTriangle.transform.position = new Vector3(targetPos.x, targetPos.y -1, 1);
        
        _player.ChangeTarget(enemy);
    }
}
