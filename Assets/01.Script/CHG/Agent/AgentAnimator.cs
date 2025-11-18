using UnityEngine;

public class AgentAnimator : MonoBehaviour
{
    private Animator _animator;
    private readonly int _attackHash = Animator.StringToHash("Attack");
    private readonly int _hurtHash = Animator.StringToHash("Hurt");

    public void Init(Animator animator)
    {
        _animator = animator;
    }
    public void AttackPlay()
    {
        _animator.SetBool(_attackHash, true);
    }
    public void AttackEnd()
    {
        _animator.SetBool(_attackHash, false);
    }
    public void HurtPlay()
    {
        _animator.SetBool(_hurtHash, true);
    }
    public void HurtEnd()
    {
        _animator.SetBool(_hurtHash, false);
    }
    
}
