using Zenject;

namespace Game.GameSystems
{
    public class WorldBoundsInstaller : Installer<float, float, WorldBoundsInstaller>
    {
        private readonly float _minXPosition;
        private readonly float _maxXPosition;

        public WorldBoundsInstaller(float minXPosition, float maxXPosition)
        {
            _minXPosition = minXPosition;
            _maxXPosition = maxXPosition;
        }

        public override void InstallBindings()
        {
            Container.BindInterfacesTo<WorldBounds>()
                .AsSingle()
                .WithArguments(_minXPosition, _maxXPosition);
        }
    }
}