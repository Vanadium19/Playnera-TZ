using System;
using R3;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Gameplay.GameSystems.Inputs
{
    public class MousePosition : IInitializable, IMousePosition
    {
        private readonly Camera _camera;

        public MousePosition(Camera camera)
        {
            _camera = camera;
        }

        public Observable<Vector2> Value { get; private set; }

        public void Initialize()
        {
            Value = Observable.EveryUpdate()
                .Select(_ => (Vector2)_camera.ScreenToWorldPoint(Input.mousePosition));
        }
    }
}