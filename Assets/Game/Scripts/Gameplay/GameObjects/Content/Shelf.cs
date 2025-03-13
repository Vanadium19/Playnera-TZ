using UnityEngine;

namespace Game.GameObjects.Content
{
    public class Shelf : IShelf
    {
        private readonly Transform _transform;
        private readonly Vector3 _offset;

        public Shelf(Transform transform, Vector3 offset)
        {
            _transform = transform;
            _offset = offset;
        }

        public Vector3 Position => _transform.position + _offset;
    }
}