using Com.PI.RunTimeReferences;
using UnityEngine;
using UnityEngine.EventSystems;


namespace Com.PI.UI {
    /// <summary>
    /// Component sets a BoolRunTimeReference value based on whether or not the Mouse is over a UI element with this on it.
    /// Useful for blocking clicks through UI elements.
    /// </summary>
    public class MouseOverUISetter : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler {
        public BoolRunTimeReference MouseOverUI;
        private bool pointerOver;
        private bool pointerDown;

        public void OnPointerEnter(PointerEventData eventData) {
            pointerOver = true;
            MouseOverUI.Value = UpdateValue();
        }

        public void OnPointerExit(PointerEventData eventData) {
            pointerOver = false;
            MouseOverUI.Value = UpdateValue();
        }

        public void OnPointerDown(PointerEventData eventData) {
            pointerDown = true;
            MouseOverUI.Value = UpdateValue();
        }

        public void OnPointerUp(PointerEventData eventData) {
            pointerDown = false;
            MouseOverUI.Value = UpdateValue();
        }

        private bool UpdateValue() { return pointerDown || pointerOver; }
        
        public void ToggleValue(bool inputValue) { MouseOverUI.Value = inputValue; }
    }
}