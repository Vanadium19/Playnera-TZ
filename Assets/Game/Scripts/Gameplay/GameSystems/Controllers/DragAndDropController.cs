using System;
using Game.GameObjects.Content;
using Game.Scripts.Gameplay.GameSystems.Inputs;
using R3;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace Game.GameSystems.Controllers
{
    public class DragAndDropController : MonoBehaviour,
        // IPointerDownHandler,
        // IPointerEnterHandler,
        // IPointerExitHandler,
        IBeginDragHandler,
        IDragHandler,
        IEndDragHandler
    {
        private IItem _item;
        private IMousePosition _mousePosition;
        private IDisposable _disposable;

        [Inject]
        public void Construct(IItem item, IMousePosition mousePosition)
        {
            _item = item;
            _mousePosition = mousePosition;
        }

        // public void OnPointerDown(PointerEventData eventData)
        // {
        //     Debug.Log("OnPointerDown");
        // }
        //
        // public void OnPointerEnter(PointerEventData eventData)
        // {
        //     Debug.Log("OnPointerEnter");
        // }
        //
        // public void OnPointerExit(PointerEventData eventData)
        // {
        //     Debug.Log("OnPointerExit");
        // }

        public void OnBeginDrag(PointerEventData eventData)
        {
            Debug.Log("OnBeginDrag");
            _disposable = _mousePosition.Value.Subscribe(position => _item.SetPosition(position));
        }

        public void OnDrag(PointerEventData eventData)
        {
            Debug.Log("OnDrag");
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            Debug.Log("OnEndDrag");
            _disposable?.Dispose();
            _item.Drop();
        }
    }
}