using System;
using R3;
using UnityEngine;
using Zenject;

namespace Game.GameSystems.Inputs
{
    public class MousePosition : IInitializable, IDisposable, IMousePosition
    {
        private readonly Camera _camera;

        private IDisposable _disposable;

        public MousePosition(Camera camera)
        {
            _camera = camera;
        }

        public Vector3 CurrentValue { get; private set; }
        public Observable<Vector3> Value { get; private set; }

        public void Initialize()
        {
            Value = Observable.EveryUpdate()
                .Select(_ => _camera.ScreenToWorldPoint(Input.mousePosition));

            _disposable = Value.Subscribe(value => CurrentValue = value);
        }

        public void Dispose()
        {
            _disposable?.Dispose();
        }
    }
}