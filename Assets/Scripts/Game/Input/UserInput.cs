using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Game.Input
{
    public class UserInput : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        public event Action<bool> ClickFire; 

        public void OnPointerDown(PointerEventData eventData)
        {
            ClickFire?.Invoke(true);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            ClickFire?.Invoke(false);
        }
    }
}