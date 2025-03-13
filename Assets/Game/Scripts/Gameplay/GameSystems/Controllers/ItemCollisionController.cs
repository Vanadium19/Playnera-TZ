using System;
using Game.GameObjects.Content;
using Game.Modules.Entities;
using UnityEngine;
using Zenject;

namespace Game.GameSystems.Controllers
{
    public class ItemCollisionController : MonoBehaviour
    {
        private IItem _item;

        [Inject]
        public void Construct(IItem item)
        {
            _item = item;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.collider.TryGetComponent(out IEntity entity))
            {
                if (entity.TryGet(out Floor floor) || entity.TryGet(out IShelf shelf))
                    _item.SetKinematic(true);
            }
        }
    }
}