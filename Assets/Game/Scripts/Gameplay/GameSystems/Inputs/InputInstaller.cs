using UnityEngine;
using Zenject;

namespace Game.Scripts.Gameplay.GameSystems.Inputs
{
    [CreateAssetMenu(fileName = "InputInstaller",
        menuName = "Zenject/New InputInstaller")]
    public class InputInstaller : ScriptableObjectInstaller
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