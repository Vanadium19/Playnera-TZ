using System;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Game.GameObjects.Content
{
    public class ItemInstaller : MonoInstaller
    {
        [SerializeField] private Transform _transform;
        [SerializeField] private Vector2 _overlapSize;

        public override void InstallBindings()
        {
            Container.Bind<Transform>()
                .FromInstance(_transform)
                .AsSingle();

            Container.BindInterfacesTo<Item>()
                .AsSingle()
                .WithArguments(_overlapSize)
                .NonLazy();
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            if (_transform == null)
                return;

            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(_transform.position, _overlapSize);
        }
#endif
    }
}