using Game.GameObjects.View;
using UnityEngine;
using Zenject;

namespace Game.GameObjects.Content
{
    public class ItemInstaller : MonoInstaller
    {
        [Header("Components")] [SerializeField] private Transform _transform;
        [SerializeField] private Rigidbody2D _rigidbody;

        [Header("Settings")] [SerializeField] private ItemParams _params;

        [Header("View")] [SerializeField] private ItemView _view;

        public override void InstallBindings()
        {
            Container.Bind<Transform>()
                .FromInstance(_transform)
                .AsSingle();

            Container.Bind<Rigidbody2D>()
                .FromInstance(_rigidbody)
                .AsSingle();

            Container.BindInterfacesTo<Item>()
                .AsSingle()
                .WithArguments(_params)
                .NonLazy();

            Container.BindInterfacesTo<ItemPresenter>()
                .AsSingle()
                .NonLazy();

            Container.Bind<ItemView>()
                .FromInstance(_view)
                .AsSingle();
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(_params.Position, _params.Size);
        }
#endif
    }
}