using Zenject;

namespace Game.GameSystems
{
    public class GameStateSchedulerInstaller : Installer<GameStateSchedulerInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<GameStateScheduler>()
                .AsSingle()
                .NonLazy();
        }
    }
}