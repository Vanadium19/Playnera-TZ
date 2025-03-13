using UnityEngine;
using Zenject;

namespace Game.GameSystems.Inputs
{
    public class InputInstaller : Installer<InputInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<MousePosition>()
                .AsSingle()
                .WithArguments(Camera.main)
                .NonLazy();
        }
    }
}