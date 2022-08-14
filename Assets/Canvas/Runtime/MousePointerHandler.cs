using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace NoodleEater.Canvas
{
    public delegate void MousePointerEnterDelegate(bool enter);
    public class MousePointerHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public MousePointerEnterDelegate OnMousePointerEnter;
        public bool IsMouseEnter { get; set; }

        public void OnPointerEnter(PointerEventData eventData)
        {
            IsMouseEnter = true;
            OnMousePointerEnter?.Invoke(true);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            IsMouseEnter = false;
            OnMousePointerEnter?.Invoke(false);
        }

        private void OnMouseEnter()
        {
            IsMouseEnter = true;
            OnMousePointerEnter?.Invoke(true);
        }

        private void OnMouseExit()
        {
            IsMouseEnter = false;
            OnMousePointerEnter?.Invoke(false);
        }
    }
}
