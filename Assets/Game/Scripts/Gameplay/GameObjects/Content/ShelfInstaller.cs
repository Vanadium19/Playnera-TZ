using UnityEngine;
using Zenject;

namespace Game.GameObjects.Content
{
    public class ShelfInstaller : MonoInstaller
    {
        [SerializeField] private Transform _transform;
        [SerializeField] private Vector3 _offset;

        public override void InstallBindings()
        {
            Container.Bind<Transform>()
                .FromInstance(_transform)
                .AsSingle();

            Container.BindInterfacesTo<Shelf>()
                .AsSingle()
                .WithArguments(_offset)
                .NonLazy();
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            if (_transform == null)
                return;

            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(_transform.position + _offset, 0.2f);
        }
#endif
    }
}