using UnityEngine;

[RequireComponent(typeof(SpriteRenderer), typeof(AgentHealth))]
public abstract class Agent : MonoBehaviour
{
    protected SpriteRenderer _spriteRen;
    protected Animator _animator;
    public AgentHealth HealthCompo { get; private set; }
    public AgentAnimator AgentAnimatorCompo { get; private set; }

    protected virtual void Awake()
    {
        HealthCompo = GetComponent<AgentHealth>();
        AgentAnimatorCompo = GetComponent<AgentAnimator>();
        _spriteRen = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
    }


}
