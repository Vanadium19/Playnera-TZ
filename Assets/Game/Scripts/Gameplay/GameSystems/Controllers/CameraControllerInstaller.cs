using UnityEngine;
using Zenject;

namespace Game.GameSystems.Controllers
{
    public class CameraControllerInstaller : Installer<CameraControllerInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<CameraController>()
                .AsSingle()
                .WithArguments(Camera.main.transform)
                .NonLazy();
        }
    }
}