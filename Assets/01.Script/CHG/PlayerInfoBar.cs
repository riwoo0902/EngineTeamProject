using TMPro;
using UnityEngine;

public class PlayerInfoBar : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI PlayerHealthText;
    [SerializeField] private TextMeshProUGUI PlayerCoinText;
    [SerializeField] private TextMeshProUGUI PlayerPower;
    [SerializeField] private TextMeshProUGUI LevelText;

    private void Start()
    {
        PlayerInfoSet();
        PlayerManager.Instance.OnValueChanged += PlayerInfoSet;
    }
    public void PlayerInfoSet()
    {
        PlayerManager pManager = PlayerManager.Instance;
        PlayerHealthText.text = pManager.MaxHealth + "/" + pManager.CurrentHealth;
        PlayerCoinText.text = pManager.Gold.ToString();
        PlayerPower.text = pManager.Power.ToString();
        LevelText.text = StageManager.Instance.Level.ToString();
    }

    private void OnDestroy()
    {
        PlayerManager.Instance.OnValueChanged -= PlayerInfoSet;
    }
}
