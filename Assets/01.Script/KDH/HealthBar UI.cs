using UnityEngine;
using UnityEngine.InputSystem;

public class HealthBarUI : MonoBehaviour
{
    [SerializeField] private Transform _bar;
    private HealthSystem _healthSystem;
    [SerializeField] private DamageData _damageData;

    private Animator _anim;

    private void Awake()
    {
        _healthSystem = GetComponentInParent<HealthSystem>();
        _anim = GetComponentInParent<Animator>();
    }

    private void Start()
    {
        UpdateBar();
        _healthSystem.OnDamaged += UpdateBar;
    }

    private void Update()
    {
        if (Keyboard.current.tKey.wasPressedThisFrame)
        {
            _healthSystem.GetDamage(_damageData);
        }

        if (Keyboard.current.rKey.wasPressedThisFrame)
        {
            _healthSystem.Deal(_damageData);
        }
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
