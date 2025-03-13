using R3;
using UnityEngine;

namespace Game.GameSystems.Inputs
{
    public interface IMousePosition
    {
        public Vector3 CurrentValue { get; }
        public Observable<Vector3> Value { get; }
    }
}