using UnityEngine;

namespace Game.GameObjects.Content
{
    public class Shelf : IShelf
    {
        private readonly float _positionY;
        private readonly float _minX;
        private readonly float _maxX;


        public Shelf(Transform transform, ShelfParams shelfParams)
        {
            _positionY = transform.position.y + shelfParams.OffsetY;
            _minX = transform.position.x - shelfParams.SizeX / 2;
            _maxX = transform.position.x + shelfParams.SizeX / 2;
        }

        public Vector3 ClampPosition(Vector3 position)
        {
            Vector3 newPosition = position;

            newPosition.y = _positionY;
            newPosition.x = Mathf.Clamp(newPosition.x, _minX, _maxX);

            return newPosition;
        }
    }
}