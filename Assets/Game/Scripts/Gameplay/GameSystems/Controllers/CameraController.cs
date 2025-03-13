using System;
using Game.Common;
using Game.GameObjects.Content;
using Game.GameSystems.Inputs;
using Game.Modules.Entities;
using R3;
using UnityEngine;
using Zenject;

namespace Game.GameSystems.Controllers
{
    public class CameraController : ILateTickable, IDisposable
    {
        private const int ColliderBufferSize = 5;

        private readonly IMousePosition _mousePosition;
        private readonly Transform _cameraTransform;

        private IDisposable _disposable;

        private Vector3 _startPosition;

        public CameraController(IMousePosition mousePosition, Transform cameraTransform)

        {
            _mousePosition = mousePosition;
            _cameraTransform = cameraTransform;
        }

        public void LateTick()
        {
            if (Input.GetMouseButtonDown(0) && CanMove())
                StartCameraMoving();

            if (Input.GetMouseButtonUp(0))
                EndCameraMoving();
        }

        private void StartCameraMoving()
        {
            _disposable = _mousePosition.Value.Subscribe(SetCameraPosition);
        }

        private void EndCameraMoving()
        {
            _startPosition = default;
            _disposable?.Dispose();
        }

        private void SetCameraPosition(Vector3 position)
        {
            if (_startPosition == default)
                _startPosition = position;

            Vector3 difference = _startPosition - position;
            _cameraTransform.position += difference;
        }

        public void Dispose()
        {
            _disposable?.Dispose();
        }

        private bool CanMove()
        {
            System.Buffers.ArrayPool<RaycastHit2D> arrayPool = System.Buffers.ArrayPool<RaycastHit2D>.Shared;
            RaycastHit2D[] results = arrayPool.Rent(ColliderBufferSize);

            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            int size = Physics2D.RaycastNonAlloc(mousePosition, Vector2.zero, results);
            bool result = true;

            for (int i = 0; i < size; i++)
            {
                if (results[i].collider.TryGetComponent(out IEntity entity) && entity.TryGet(out IItem item))
                {
                    Debug.Log("Попал В Предмет");
                    result = false;
                }
            }

            if (result)
                Debug.Log("Не Попал В Предмет");

            arrayPool.Return(results);
            return result;
        }
    }
}