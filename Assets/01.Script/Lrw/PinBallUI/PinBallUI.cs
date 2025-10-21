using Lrw_CustomReadonly;
using Lrw_PinBall;
using UnityEngine;
using UnityEngine.UI;

namespace Lrw_PinBallUI
{
    public class PinBallUI : MonoBehaviour
    {
        [SerializeField,ReadOnly] private PinBallSO pinBallSO;
        private Image _image;
        private void Awake()
        {
            _image = GetComponent<Image>();
        }

        public void SetPinBallData(PinBallSO a)
        {
            _image.sprite = a.PinBallImage;
        }
        

    }
}

