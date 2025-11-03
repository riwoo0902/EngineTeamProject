using UnityEngine;

[RequireComponent(typeof(SpriteRenderer), typeof(AgentHealth))]
public abstract class Agent : MonoBehaviour
{
    protected SpriteRenderer _spriteRen;
    public AgentHealth HealthCompo { get; private set; }

    protected virtual void Awake()
    {
        HealthCompo = GetComponent<AgentHealth>();
        _spriteRen = GetComponent<SpriteRenderer>();
    }


}
