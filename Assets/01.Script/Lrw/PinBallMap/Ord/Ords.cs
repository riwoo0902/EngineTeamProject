using UnityEngine;
using UnityEngine.Events;

public class Ords : MonoBehaviour
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
