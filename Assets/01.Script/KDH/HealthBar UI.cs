using UnityEngine;

public class HealthBarUI : MonoBehaviour
{
    [SerializeField] private Transform _bar;
    private HealthSystem _healthSystem;

    private void Awake()
    {
        _healthSystem = GetComponentInParent<HealthSystem>();
    }

    private void Start()
    {
        UpdateBar();
        _healthSystem.OnDamaged += UpdateBar;
    }

    private void UpdateBar()
    {
        float xScale = _healthSystem.GetNormalizeHelath();
        _bar.localScale = new Vector3(xScale, _bar.localScale.y, _bar.localScale.z);
    }

    private void OnDestroy()
    {
        _healthSystem.OnDamaged -= UpdateBar;
    }

}
