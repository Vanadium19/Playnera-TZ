using System;
using Game.Common;
using Game.GameSystems.Inputs;
using R3;
using UnityEngine;
using Zenject;

namespace Game.GameSystems.Controllers
{
    public class CameraController : ILateTickable, IDisposable
    {
        private readonly IGameStateScheduler _gameStateScheduler;
        private readonly IMousePosition _mousePosition;
        private readonly Transform _cameraTransform;

        private readonly CompositeDisposable _disposable = new();

        private Vector3 _startPosition;

        public CameraController(IGameStateScheduler gameStateScheduler,
            IMousePosition mousePosition,
            Transform cameraTransform)

        {
            _mousePosition = mousePosition;
            _cameraTransform = cameraTransform;
            _gameStateScheduler = gameStateScheduler;
        }

        public void LateTick()
        {
            if (Input.GetMouseButtonDown(0) && _gameStateScheduler.CurrentState == GameState.CalmState)
                StartCameraMoving();

            if (Input.GetMouseButtonUp(0) && _gameStateScheduler.CurrentState == GameState.CameraMoving)
                EndCameraMoving();
        }

        private void StartCameraMoving()
        {
            _gameStateScheduler.ChangeState(GameState.CameraMoving);

            _mousePosition.Value.Subscribe(SetCameraPosition)
                .AddTo(_disposable);

            _gameStateScheduler.StateObservable.Where(state => state != GameState.CameraMoving)
                .Subscribe(_ => EndCameraMoving())
                .AddTo(_disposable);
        }

        private void EndCameraMoving()
        {
            if (_gameStateScheduler.CurrentState == GameState.CameraMoving)
                _gameStateScheduler.ChangeState(GameState.CalmState);

            _startPosition = default;
            _disposable.Clear();
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
            _disposable.Dispose();
        }
    }
}