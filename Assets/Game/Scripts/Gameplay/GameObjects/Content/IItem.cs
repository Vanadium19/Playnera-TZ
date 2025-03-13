using UnityEngine;

namespace Game.GameObjects.Content
{
    public interface IItem
    {
        public void Drop();
        public void SetPosition(Vector3 position);
    }
}