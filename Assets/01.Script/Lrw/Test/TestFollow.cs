using UnityEngine;

public class TestFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3Int targetPos;
    private void FixedUpdate()
    {
        transform.position = target.position + targetPos;
    }
}
