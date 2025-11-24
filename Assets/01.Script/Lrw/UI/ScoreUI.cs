using _01.Script.Lrw.EventBus.EventBusSystem.CoreSystem;
using _01.Script.Lrw.EventBus.EventBusSystem.Events;
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
            EventBus<ScoreEvent>.OnEvent += SetText;
        }

        private void OnDestroy()
        {
            EventBus<ScoreEvent>.OnEvent -= SetText;
        }

        private void SetText(ScoreEvent text)
        {
            _text.text = $"Score:{text.Score}";
        }
    }
}