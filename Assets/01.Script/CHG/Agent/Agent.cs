using UnityEngine;

public class Agent : MonoBehaviour
{
    public AgentHealth HealthCompo { get; protected set; }

    protected virtual void Awake()
    {
        HealthCompo = GetComponent<AgentHealth>();
    }


}
