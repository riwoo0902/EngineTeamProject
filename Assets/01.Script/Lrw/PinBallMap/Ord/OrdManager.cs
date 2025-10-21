using UnityEngine;
using UnityEngine.Events;

public class OrdManager : MonoBehaviour
{
    public static UnityEvent OrdCollisionEvent;
    private void Awake()
    {
        OrdCollisionEvent = null;
    }
    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        
    }


}
