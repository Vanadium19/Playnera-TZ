using UnityEngine;

namespace Game.GameObjects.Content
{
    public interface IItem
    {
        public bool IsDropping { get; }

        public void Pickup();
        public void Drop();
        public void SetPosition(Vector3 position);
        public void SetPositionForced(Vector3 position);
        public void SetOnShelf(IShelf shelf);
    }
}