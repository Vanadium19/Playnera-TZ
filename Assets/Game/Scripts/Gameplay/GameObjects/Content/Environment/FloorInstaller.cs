using Zenject;

namespace Game.GameObjects.Content
{
    public class FloorInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<Floor>()
                .AsSingle()
                .NonLazy();
        }
    }
}