using Game.Modules.Entities;
using UnityEngine;

namespace Game.GameObjects.Content
{
    public class Item : IItem
    {
        private const int ColliderBufferSize = 5;
        private const float OverlapAngle = 0f;

        private readonly Transform _transform;
        private readonly Vector2 _size;

        public Item(Transform transform, Vector2 size)
        {
            _transform = transform;
            _size = size;
        }

        public void Drop()
        {
            System.Buffers.ArrayPool<Collider2D> arrayPool = System.Buffers.ArrayPool<Collider2D>.Shared;
            Collider2D[] colliders = arrayPool.Rent(ColliderBufferSize);

            int size = Physics2D.OverlapBoxNonAlloc(_transform.position, _size, OverlapAngle, colliders);

            for (int i = 0; i < size; i++)
            {
                if (colliders[i].TryGetComponent(out IEntity entity))
                {
                    if (entity.TryGet(out IShelf shelf))
                    {
                        SetPosition(shelf.Position);
                    }
                }
            }

            arrayPool.Return(colliders);
        }

        public void SetPosition(Vector3 position)
        {
            _transform.position = position;
        }
    }
}