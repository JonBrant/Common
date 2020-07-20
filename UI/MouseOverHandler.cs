using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;


namespace Com.PI.UI {
    public class MouseOverHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
        [Serializable] public class OnMouseEnterUnityEvent : UnityEvent<GameObject> {
        }

        [Serializable] public class OnMouseExitUnityEvent : UnityEvent<GameObject> {
        }

        [SerializeField] public OnMouseEnterUnityEvent OnMouseEnter;
        [SerializeField] public OnMouseExitUnityEvent OnMouseExit;

        public void OnPointerEnter(PointerEventData eventData) {
            OnMouseEnter?.Invoke(gameObject);
        }

        public void OnPointerExit(PointerEventData eventData) {
            OnMouseExit?.Invoke(gameObject);
        }
    }
}