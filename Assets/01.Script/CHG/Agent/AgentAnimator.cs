using UnityEngine;

public class AgentAnimator : MonoBehaviour
{
    private Animator _animator;
    private readonly int _attackHash = Animator.StringToHash("Attack");

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

    
}
