using DG.Tweening;
using DG.Tweening.Core;
using UnityEngine;

namespace _01.Script.Lrw.UI
{
    public class UIOnOff : MonoBehaviour
    {
        [SerializeField] private Transform offTarget;
        [SerializeField] private float duration = 1f;
        [SerializeField] private Ease onease = Ease.OutQuart;
        [SerializeField] private Ease offease = Ease.OutQuart;
        private Vector3 _onPos;
        private Vector3 _offPos;
        
        private bool _isOn = false;
        private bool _isMove = false;
        private void Awake()
        {
            _onPos =  transform.position;
            _offPos = offTarget.position;
            _isOn = false;
        }
        
        [ContextMenu("ChangeOnOff")]
        public void ChangeOnOff()
        {
            SetOnOff(!_isOn);
        }
        
        public void SetOnOff(bool on)
        {
            if(_isMove) return;
            _isOn = on;
            _isMove = true;
            transform.DOMove(_isOn ? _offPos : _onPos, duration).SetEase(_isOn ? offease : onease);
            transform.DOScale(_isOn ? Vector3.zero : Vector3.one, duration)
                .SetEase(_isOn ? offease : onease).OnComplete(() =>
                {
                    _isMove = false;
                });
        }
        
    }
}