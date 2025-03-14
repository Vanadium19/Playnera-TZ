using UnityEngine;

namespace Game.GameSystems
{
    public interface IWorldBounds
    {
        public Vector3 Clamp(Vector3 position);
    }
}