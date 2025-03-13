using DG.Tweening;
using Game.Modules.Entities;
using R3;
using UnityEngine;

namespace Game.GameObjects.Content
{
    public class Item : IItem, IItemObservable
    {
        private const int ColliderBufferSize = 5;
        private const float ClampDelay = 0.2f;
        private const float OverlapAngle = 0f;

        private readonly Transform _transform;
        private readonly Rigidbody2D _rigidbody;
        private readonly ItemParams _params;

        private readonly ReactiveCommand _dropCommand = new();
        private readonly ReactiveCommand _pickupCommand = new();

        private Tween _moveTween;
        private bool _isDropping;

        public Item(Transform transform, Rigidbody2D rigidbody, ItemParams itemParams)
        {
            _transform = transform;
            _rigidbody = rigidbody;
            _params = itemParams;
        }

        public Observable<Unit> DropObservable => _dropCommand;
        public Observable<Unit> PickupObservable => _pickupCommand;
        public bool IsDropping => _isDropping;

        public void Pickup()
        {
            SetKinematic();
            _moveTween?.Kill();
            _pickupCommand.Execute(Unit.Default);
        }

        public void Drop()
        {
            System.Buffers.ArrayPool<Collider2D> arrayPool = System.Buffers.ArrayPool<Collider2D>.Shared;
            Collider2D[] colliders = arrayPool.Rent(ColliderBufferSize);

            _isDropping = true;
            _rigidbody.isKinematic = false;

            int size = Physics2D.OverlapBoxNonAlloc(_params.Position, _params.Size, _params.Angle, colliders);

            for (int i = 0; i < size; i++)
            {
                if (colliders[i].TryGetComponent(out IEntity entity))
                {
                    if (entity.TryGet(out IShelf shelf))
                        SetOnShelf(shelf);
                    else if (entity.TryGet(out Floor floor))
                        SetKinematic();
                }
            }

            arrayPool.Return(colliders);
            _dropCommand.Execute(Unit.Default);
        }

        public void SetPositionForced(Vector3 position)
        {
            _transform.position = position;
        }

        public void SetPosition(Vector3 position)
        {
            SetKinematic();
            _moveTween = _transform.DOMove(position, ClampDelay).SetEase(Ease.Linear);
        }

        public void SetOnShelf(IShelf shelf)
        {
            Vector3 position = shelf.ClampPosition(_transform.position);

            SetPosition(position);
        }

        private void SetKinematic()
        {
            _isDropping = false;
            _rigidbody.velocity = Vector2.zero;
            _rigidbody.angularVelocity = 0f;
            _rigidbody.isKinematic = true;
        }
    }
}