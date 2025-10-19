using UnityEngine;

public class EnemyTargeting : MonoBehaviour
{
    [SerializeField] private GameObject RedTriangle; //타겟 표시용, 이름 바꿔야함
    private SpriteRenderer _spren; //이름 바꿔야함
    private Color _sprenColor; //이름 바꿔야함
    private Player _player;
    private void Awake()
    {
        _player = GetComponent<Player>();
        _spren = RedTriangle.GetComponent<SpriteRenderer>();
        _sprenColor = _spren.color;
        _sprenColor.a = 0;
        _spren.color = _sprenColor; //씬 시작할때? 실행 시점 나중에 봐야할듯
    }



    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D rayHit = Physics2D.Raycast(pos, Vector2.zero, 0f);

            if (rayHit.collider != null)
            {
                rayHit.collider.TryGetComponent<C_Enemy>(out C_Enemy enemy);
                TargetSet(enemy);
            }
            //else 
            //{
            //    _sprenColor.a = 0;
            //    _spren.color = _sprenColor;
            //}
        }
    }

    private void TargetSet(C_Enemy enemy)
    {
        _sprenColor.a = 1;
        _spren.color = _sprenColor;
        Vector3 targetPos = enemy.transform.position;
        RedTriangle.transform.position = new Vector3(targetPos.x, targetPos.y -1, 1);
        
        _player.ChangeTarget(enemy);
        
    }



}
