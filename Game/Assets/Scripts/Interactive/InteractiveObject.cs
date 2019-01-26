using UnityEngine;
using UnityEngine.EventSystems;

namespace Interactive
{
    /// <summary>
    /// Класс для объектов, которые что-то делают
    /// </summary>
    public abstract class InteractiveObject : MonoBehaviour, IPointerClickHandler
    {
        /// <summary>
        /// Взаимодействует с объектом
        /// </summary>
        public abstract void Interact();

        public void OnPointerClick(PointerEventData eventData)
        {
            Interact();
        }
    }
}