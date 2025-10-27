using UnityEngine;

public class HealthBarUI : MonoBehaviour
{
    [SerializeField] private HealthSystem healthSystem; // 체력 시스템 연결
    [SerializeField] private Transform healthBar;       // 체력바 오브젝트 (스케일 변경할 것)
    [SerializeField] private CharacterData characterData;
    private float originalScaleX;

    private void Start()
    {
        if (healthBar == null || healthSystem == null) return;

        originalScaleX = healthBar.localScale.x;

        // 이벤트 등록

        // 초기 체력 표시
        UpdateHealthBar();
    }

    private void UpdateHealthBar()
    {
        float healthPercent = (float)healthSystem.currentHealth / characterData.maxHealth;
        Vector3 newScale = healthBar.localScale;
        newScale.x = originalScaleX * healthPercent;
        healthBar.localScale = newScale;
    }

    private void OnDestroy()
    {
        if (healthSystem != null)
        {

        }
    }
}