using System;
using UnityEngine;
using Zenject;

namespace Game.Modules.Entities
{
    public class Entity : MonoBehaviour, IEntity
    {
        [SerializeField] private GameObjectContext _context;

        private DiContainer _container;

        public event Action<Entity> OnDestroyed;

        private void Awake()
        {
            _container = _context.Container;
        }

        public T Get<T>()
        {
            return _container.Resolve<T>();
        }

        public bool TryGet<T>(out T value) where T : class
        {
            value = _container.TryResolve<T>();

            return value != null;
        }
    }
}