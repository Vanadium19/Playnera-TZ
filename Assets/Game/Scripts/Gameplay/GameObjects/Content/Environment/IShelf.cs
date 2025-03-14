using UnityEngine;

namespace Game.GameObjects.Content
{
    public interface IShelf
    {
        public Vector3 ClampPosition(Vector3 position);
    }
}