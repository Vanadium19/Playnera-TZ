using UnityEngine;

namespace Game.GameSystems
{
    public class WorldBounds : IWorldBounds
    {
        private readonly float _minX;
        private readonly float _maxX;

        public WorldBounds(float minX, float maxX)
        {
            _minX = minX;
            _maxX = maxX;
        }

        public Vector3 Clamp(Vector3 position)
        {
            Vector3 newPosition = position;
            newPosition.x = Mathf.Clamp(newPosition.x, _minX, _maxX);

            return newPosition;
        }
    }
}