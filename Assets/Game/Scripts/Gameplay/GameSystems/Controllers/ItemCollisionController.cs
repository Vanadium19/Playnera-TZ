using System;
using Game.Common;
using Game.GameObjects.Content;
using Game.Modules.Entities;
using UnityEngine;
using Zenject;

namespace Game.GameSystems.Controllers
{
    public class ItemCollisionController : MonoBehaviour
    {
        private IItem _item;
        private IGameStateScheduler _gameStateScheduler;

        [Inject]
        public void Construct(IItem item, IGameStateScheduler gameStateScheduler)
        {
            _item = item;
            _gameStateScheduler = gameStateScheduler;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (_gameStateScheduler.CurrentState == GameState.ItemMoving)
                return;

            if (other.collider.TryGetComponent(out IEntity entity))
            {
                if (entity.TryGet(out Floor floor) || entity.TryGet(out IShelf shelf))
                    Debug.Log("Upal");
            }
        }
    }
}