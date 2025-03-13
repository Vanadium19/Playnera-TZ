using UnityEngine;
using Zenject;

namespace Game.GameObjects.Content
{
    public class ShelfInstaller : MonoInstaller
    {
        [SerializeField] private Transform _transform;
        [SerializeField] private float _offsetY;

        public override void InstallBindings()
        {
            Container.Bind<Transform>()
                .FromInstance(_transform)
                .AsSingle();

            Container.BindInterfacesTo<Shelf>()
                .AsSingle()
                .WithArguments(_offsetY)
                .NonLazy();
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            if (_transform == null)
                return;

            Vector3 position = _transform.position;
            position.y += _offsetY;

            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(position, 0.2f);
        }
#endif
    }
}