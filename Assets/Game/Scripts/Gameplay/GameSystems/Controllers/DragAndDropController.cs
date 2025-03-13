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
        IPointerEnterHandler,
        IPointerExitHandler,
        IBeginDragHandler,
        IDragHandler,
        IEndDragHandler
    {
        private readonly Vector3 _axis = new Vector3(1f, 1f, 0f);

        private IItem _item;
        private IMousePosition _mousePosition;
        private IGameStateScheduler _gameStateScheduler;

        private IDisposable _disposable;

        [Inject]
        public void Construct(IItem item,
            IMousePosition mousePosition,
            IGameStateScheduler gameStateScheduler)
        {
            _item = item;
            _mousePosition = mousePosition;
            _gameStateScheduler = gameStateScheduler;
        }

        // public void OnPointerDown(PointerEventData eventData)
        // {
        //     Debug.Log("OnPointerDown");
        // }

        public void OnPointerEnter(PointerEventData eventData)
        {
            Debug.Log("OnPointerEnter");
            _gameStateScheduler.ChangeState(GameState.ReadyToMoveItem);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            Debug.Log("OnPointerExit");
            _gameStateScheduler.ChangeState(GameState.CalmState);
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            Debug.Log("OnBeginDrag");
            _item.Pickup();
            _gameStateScheduler.ChangeState(GameState.ItemMoving);
            _disposable = _mousePosition.Value.Subscribe(SetItemPosition);
        }

        public void OnDrag(PointerEventData eventData)
        {
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            Debug.Log("OnEndDrag");
            _gameStateScheduler.ChangeState(GameState.CalmState);
            _disposable?.Dispose();
            _item.Drop();
        }

        private void SetItemPosition(Vector3 position)
        {
            position = Vector3.Scale(_axis, position);

            _item.SetPosition(position);
        }
    }
}