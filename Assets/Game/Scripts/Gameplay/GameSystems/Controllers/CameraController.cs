using System;
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

        private readonly IInputStrategy _input;
        private readonly IWorldBounds _worldBounds;
        private readonly IMousePosition _mousePosition;
        private readonly Transform _cameraTransform;

        private IDisposable _disposable;
        private Vector3 _dragPosition;

        public CameraController(IWorldBounds worldBounds,
            IMousePosition mousePosition,
            Transform cameraTransform,
            IInputStrategy input)
        {
            _input = input;
            _worldBounds = worldBounds;
            _mousePosition = mousePosition;
            _cameraTransform = cameraTransform;
        }

        public void LateTick()
        {
            if (_input.StartClick() && CanMove())
                StartCameraMoving();

            if (_input.EndClick())
                EndCameraMoving();
        }

        private void StartCameraMoving()
        {
            _dragPosition = _mousePosition.CurrentValue;

            _disposable = _mousePosition.Value.Subscribe(SetCameraPosition);
        }

        private void EndCameraMoving()
        {
            _dragPosition = default;
            _disposable?.Dispose();
        }

        private void SetCameraPosition(Vector3 position)
        {
            Vector3 newPosition = _cameraTransform.position;
            Vector3 difference = _dragPosition - position;

            newPosition += difference;
            newPosition = _worldBounds.Clamp(newPosition);
            newPosition.y = _cameraTransform.position.y;

            _cameraTransform.position = newPosition;
        }

        public void Dispose()
        {
            _disposable?.Dispose();
        }

        private bool CanMove()
        {
            System.Buffers.ArrayPool<RaycastHit2D> arrayPool = System.Buffers.ArrayPool<RaycastHit2D>.Shared;
            RaycastHit2D[] results = arrayPool.Rent(ColliderBufferSize);

            bool result = true;
            int size = Physics2D.RaycastNonAlloc(_mousePosition.CurrentValue, Vector2.zero, results);

            for (int i = 0; i < size; i++)
            {
                if (results[i].collider.TryGetComponent(out IEntity entity) && entity.TryGet(out IItem item))
                {
                    result = false;
                }
            }

            arrayPool.Return(results);
            return result;
        }
    }
}