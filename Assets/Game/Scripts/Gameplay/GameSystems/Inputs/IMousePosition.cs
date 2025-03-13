using R3;
using UnityEngine;

namespace Game.GameSystems.Inputs
{
    public interface IMousePosition
    {
        public Observable<Vector3> Value { get; }
    }
}