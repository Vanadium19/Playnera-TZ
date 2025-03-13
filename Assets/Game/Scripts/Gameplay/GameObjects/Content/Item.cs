using Game.Modules.Entities;
using R3;
using UnityEngine;

namespace Game.GameObjects.Content
{
    public class Item : IItem, IItemObservable
    {
        private const int ColliderBufferSize = 5;
        private const float OverlapAngle = 0f;

        private readonly Transform _transform;
        private readonly Rigidbody2D _rigidbody;
        private readonly Vector2 _size;

        private readonly ReactiveCommand _dropCommand = new();
        private readonly ReactiveCommand _pickupCommand = new();

        public Item(Transform transform, Rigidbody2D rigidbody, Vector2 size)
        {
            _transform = transform;
            _rigidbody = rigidbody;
            _size = size;
        }

        public Observable<Unit> DropObservable => _dropCommand;
        public Observable<Unit> PickupObservable => _pickupCommand;

        public void Pickup()
        {
            _rigidbody.isKinematic = true;
            _pickupCommand.Execute(Unit.Default);
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
            _dropCommand.Execute(Unit.Default);
        }

        public void SetPosition(Vector3 position)
        {
            if (_rigidbody.isKinematic == false)
                _rigidbody.isKinematic = true;

            _transform.position = position;
        }

        private void SetOnShelf(IShelf shelf)
        {
            var position = _transform.position;
            position.y = shelf.PositionY;

            _transform.position = position;
        }
    }
}