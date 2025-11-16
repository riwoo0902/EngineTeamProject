using UnityEngine;

public class Repeat : MonoBehaviour
{
    [SerializeField] private float speed = 0.1f;
    [SerializeField] private float xTarget;

    private Vector3 startPos;

    private void Start()
    {
        startPos = transform.position;
    }

    private void Update()
    {
        transform.position += Vector3.right * speed * Time.deltaTime;
        
        if(transform.position.x <= xTarget)
        {
            transform.position = startPos;
        }
    }

}
