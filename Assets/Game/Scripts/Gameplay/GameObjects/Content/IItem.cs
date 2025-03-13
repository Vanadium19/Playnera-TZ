using UnityEngine;

namespace Game.GameObjects.Content
{
    public interface IItem
    {
        public void Pickup();
        public void Drop();
        public void SetPosition(Vector3 position);
    }
}