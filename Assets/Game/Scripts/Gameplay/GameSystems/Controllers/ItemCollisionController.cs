﻿using System;
using Game.Common;
using Game.GameObjects.Content;
using Game.Modules.Entities;
using UnityEngine;
using UnityEngine.Assertions.Must;
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
            if (!_item.IsDropping)
                return;

            if (other.collider.TryGetComponent(out IEntity entity))
            {
                if (entity.TryGet(out Floor floor))
                {
                    _item.SetPosition(other.contacts[0].point);
                }
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!_item.IsDropping)
                return;

            if (other.TryGetComponent(out IEntity entity))
            {
                if (entity.TryGet(out IShelf shelf))
                {
                    _item.SetOnShelf(shelf);
                }
            }
        }
    }
}