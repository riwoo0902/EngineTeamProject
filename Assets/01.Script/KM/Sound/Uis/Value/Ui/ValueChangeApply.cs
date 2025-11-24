using _01.Script.Lrw.CustomSoundManager;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _01.Script.KM.Sound.Uis.Value.Ui
{
    public enum SetValueTarget
    {
        Master,
        SFX,
        BackGround
    }

    [DefaultExecutionOrder(1)]
    public class ValueChangeApply : MonoBehaviour
    {
        [SerializeField] private TMP_Text valueText;
        [SerializeField] private SoundType audioType;
        private Slider _slider;

        private void Start()
        {
            _slider = GetComponent<Slider>();
            _slider.onValueChanged.AddListener(SettingValue);
            SettingValue(_slider.value);

        }

        private void OnDestroy()
        {
            _slider.onValueChanged.RemoveAllListeners();
        }

        private void SettingValue(float value)
        {
            if (audioType == SoundType.Master)
            {
                SoundManager.Instance.MasterVolume = value;
            }

            if (audioType == SoundType.SFX)
            {
                SoundManager.Instance.SFXVolume = value;
            }

            if (audioType == SoundType.BackGround)
            {
                SoundManager.Instance.BGMVolume = value;
            }
            valueText.text = value.ToString();
        }
    }

}