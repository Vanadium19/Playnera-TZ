using UnityEngine;

namespace Game.GameObjects.Content
{
    public class Shelf : IShelf
    {
        private readonly Transform _transform;
        private readonly float _offsetY;

        public Shelf(Transform transform, float offsetY)
        {
            _transform = transform;
            _offsetY = offsetY;
        }

        public float PositionY => _transform.position.y + _offsetY;
    }
}