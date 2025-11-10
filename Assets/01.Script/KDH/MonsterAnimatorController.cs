using UnityEngine;

public class MonsterAnimatorController : MonoBehaviour
{
    private Animator _anim;
    private HealthSystem _health;
    private bool _isAttacking = false;

    private void Awake()
    {
        _anim = GetComponent<Animator>();
        _health = GetComponent<HealthSystem>();
    }

    private void Start()
    {
        // HealthSystem에서 OnDamaged 이벤트 구독
        _health.OnDamaged += OnHit;
        _health.OnDeal += Attack;
    }

    private void OnDestroy()
    {
        _health.OnDamaged -= OnHit;
        _health.OnDeal -= Attack;
    }

    private void OnHit()
    {
        // 공격 중이면 Hit 중복 방지
        if (_isAttacking) return;
        _anim.SetBool("hit", true);
    }

    private void ResetHit()
    {
        _anim.SetBool("hit", false);
    }

    private void Attack()
    {
        if (_isAttacking) return;

        _isAttacking = true;
        _anim.SetBool("attack", true);
    }

    private void ResetAttack()
    {
        _isAttacking = false;
        _anim.SetBool("attack", false);
    }
}
