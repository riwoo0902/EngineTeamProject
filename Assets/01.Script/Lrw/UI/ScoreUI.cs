using _01.Script.Lrw.Manager;
using TMPro;
using UnityEngine;

namespace _01.Script.Lrw.UI
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class ScoreUI : MonoBehaviour
    {
        private TextMeshProUGUI _text;
        private void Awake()
        {
            _text = GetComponent<TextMeshProUGUI>();
        }

        private void Update()
        {
            _text.text = $"Score : {ScoreManager.Instance.Score}";
        }
    }
}