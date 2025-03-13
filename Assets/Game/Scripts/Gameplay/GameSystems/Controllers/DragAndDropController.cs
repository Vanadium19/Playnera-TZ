using System;
using Game.Common;
using Game.GameObjects.Content;
using Game.GameSystems.Inputs;
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
        private readonly Vector3 _axis = new Vector3(1f, 1f, 0f);

        private IItem _item;
        private IMousePosition _mousePosition;

        private IDisposable _disposable;

        [Inject]
        public void Construct(IItem item,
            IMousePosition mousePosition)
        {
            _item = item;
            _mousePosition = mousePosition;
        }

        // public void OnPointerDown(PointerEventData eventData)
        // {
        //     Debug.Log("OnPointerDown");
        // }

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
            _item.Pickup();
            _disposable = _mousePosition.Value.Subscribe(SetItemPosition);
        }

        public void OnDrag(PointerEventData eventData)
        {
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            Debug.Log("OnEndDrag");
            _disposable?.Dispose();
            _item.Drop();
        }

        private void SetItemPosition(Vector3 position)
        {
            position = Vector3.Scale(_axis, position);

            _item.SetPositionForced(position);
        }
    }
}