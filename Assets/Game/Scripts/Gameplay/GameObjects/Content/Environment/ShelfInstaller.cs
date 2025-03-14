using UnityEngine;
using Zenject;

namespace Game.GameObjects.Content
{
    public class ShelfInstaller : MonoInstaller
    {
        [SerializeField] private Transform _transform;
        [SerializeField] private ShelfParams _params;

        public override void InstallBindings()
        {
            Container.Bind<Transform>()
                .FromInstance(_transform)
                .AsSingle();

            Container.BindInterfacesTo<Shelf>()
                .AsSingle()
                .WithArguments(_params)
                .NonLazy();
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            if (_transform == null)
                return;

            Vector3 position = _transform.position;
            position.y += _params.OffsetY;

            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(position, new Vector3(_params.SizeX, 0.2f, 0f));
        }
#endif
    }
}