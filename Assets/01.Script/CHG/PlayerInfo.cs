using TMPro;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI PlayerHealthText;
    [SerializeField] private TextMeshProUGUI PlayerCoinText;
    [SerializeField] private TextMeshProUGUI PlayerPower;
    [SerializeField] private TextMeshProUGUI LevelText;

    private void PlayerInfoSet()
    {
        PlayerManager pManager = PlayerManager.Instance;
        PlayerHealthText.text = pManager.MaxHealth + "/" + pManager.CurrentHealth;
        PlayerCoinText.text = pManager.Gold.ToString();
        PlayerPower.text = pManager.Power.ToString();
        LevelText.text = StageManager.Instance.Level.ToString();
    }
}
