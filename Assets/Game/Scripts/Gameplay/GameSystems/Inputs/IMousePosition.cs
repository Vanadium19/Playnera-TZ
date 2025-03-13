using R3;
using UnityEngine;

namespace Game.Scripts.Gameplay.GameSystems.Inputs
{
    public interface IMousePosition
    {
        public Observable<Vector2> Value { get; }
    }
}