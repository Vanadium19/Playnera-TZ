using R3;

namespace Game.GameObjects.Content
{
    public interface IItemObservable
    {
        public Observable<Unit> DropObservable { get; }
        public Observable<Unit> PickupObservable { get; }
    }
}