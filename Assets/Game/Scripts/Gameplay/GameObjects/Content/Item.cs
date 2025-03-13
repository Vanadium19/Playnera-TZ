using Game.Modules.Entities;
using UnityEngine;

namespace Game.GameObjects.Content
{
    public class Item : IItem
    {
        private const int ColliderBufferSize = 5;
        private const float OverlapAngle = 0f;

        private readonly Transform _transform;
        private readonly Rigidbody2D _rigidbody;
        private readonly Vector2 _size;

        public Item(Transform transform, Rigidbody2D rigidbody, Vector2 size)
        {
            _transform = transform;
            _rigidbody = rigidbody;
            _size = size;
        }

        public void Drop()
        {
            System.Buffers.ArrayPool<Collider2D> arrayPool = System.Buffers.ArrayPool<Collider2D>.Shared;
            Collider2D[] colliders = arrayPool.Rent(ColliderBufferSize);

            bool findParent = false;
            int size = Physics2D.OverlapBoxNonAlloc(_transform.position, _size, OverlapAngle, colliders);

            for (int i = 0; i < size; i++)
            {
                if (colliders[i].TryGetComponent(out IEntity entity))
                {
                    findParent = entity.TryGet(out IShelf shelf) || entity.TryGet(out Floor floor);

                    if (findParent)
                    {
                        if (shelf != null)
                            SetOnShelf(shelf);

                        break;
                    }
                }
            }

            arrayPool.Return(colliders);
            _rigidbody.isKinematic = findParent;
        }

        public void SetPosition(Vector3 position)
        {
            if (!_rigidbody.isKinematic)
                _rigidbody.isKinematic = true;

            _transform.position = position;
        }

        public void SetKinematic(bool value)
        {
            _rigidbody.isKinematic = value;
        }

        private void SetOnShelf(IShelf shelf)
        {
            var position = _transform.position;
            position.y = shelf.PositionY;

            _transform.position = position;
        }
    }
}