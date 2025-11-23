using TMPro;
using Unity.Mathematics;
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
        [SerializeField] private TMP_InputField writeValueText;
        [SerializeField] private SetValueTarget targetEnum;
        private Slider _slider;

        private void Start()
        {
            _slider = GetComponent<Slider>();
            if (_slider.value > 0.01f)
                valueText.text = Mathf.RoundToInt(_slider.value * 100).ToString();
            else
                valueText.text = "0";
            SettingValue(_slider.value);
        }

        public void ValueChange()
        {
            if (_slider.value > 0.01f)
                valueText.text = Mathf.RoundToInt(_slider.value * 100).ToString();
            else
                valueText.text = "0";
            SettingValue(_slider.value);
        }

        public void SetValueChange()
        {
            Debug.Log(writeValueText.GetType());
            if (int.TryParse(writeValueText.text.ToString().Trim(), out int ChangeValue))
            {
                Debug.Log(ChangeValue);
                _slider.value = math.clamp(ChangeValue, 0, 100) / 100f;
                if (_slider.value > 0.01f)
                    valueText.text = Mathf.RoundToInt(_slider.value * 100).ToString();
                else
                    valueText.text = "0";
                writeValueText.text = "";
                SettingValue(_slider.value);
            }
        }


        private void SettingValue(float value)
        {
            if (targetEnum == SetValueTarget.Master)
            {
                SoundMananger.Instacne.MasterChange(value);
            }

            if (targetEnum == SetValueTarget.SFX)
            {
                SoundMananger.Instacne.SFXChange(value);
            }

            if (targetEnum == SetValueTarget.BackGround)
            {
                SoundMananger.Instacne.BackGroundChange(value);
            }
        }
    }

}