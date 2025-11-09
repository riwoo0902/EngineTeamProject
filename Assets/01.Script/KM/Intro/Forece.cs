using UnityEngine;

public class Forece : MonoBehaviour
{
    private Rigidbody2D _rbCompo;
    [SerializeField] private float force = 100f;
    private void Start()
    {
        _rbCompo = GetComponent<Rigidbody2D>();
        _rbCompo.AddForceX(force, ForceMode2D.Force);
    }
}
