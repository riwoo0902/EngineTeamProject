using UnityEngine;
using UnityEngine.EventSystems;

namespace _01.Script.Lrw.UI
{
    public class MouseCheckUI : MonoSingleton<MouseCheckUI>,IPointerEnterHandler,IPointerExitHandler
    {
        [field: SerializeField] public bool MouseOn { get; private set; } = false;

        public void OnPointerEnter(PointerEventData eventData)
        {
            MouseOn = true;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            MouseOn = false;
        }
    }
}