using System;
using Game.GameObjects.Content;
using R3;
using Zenject;

namespace Game.GameObjects.View
{
    public class ItemPresenter : IInitializable, IDisposable
    {
        private readonly IItemObservable _item;
        private readonly ItemView _itemView;

        private IDisposable _disposables;

        public ItemPresenter(IItemObservable item, ItemView itemView)
        {
            _item = item;
            _itemView = itemView;
        }

        public void Initialize()
        {
            DisposableBuilder builder = Disposable.CreateBuilder();

            _item.PickupObservable.Subscribe(_ => _itemView.PickUp()).AddTo(ref builder);
            _item.DropObservable.Subscribe(_ => _itemView.Drop()).AddTo(ref builder);

            _disposables = builder.Build();
        }

        public void Dispose()
        {
            _disposables?.Dispose();
        }
    }
}