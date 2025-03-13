using Game.GameSystems;
using Game.GameSystems.Controllers;
using Game.GameSystems.Inputs;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Gameplay
{
    [CreateAssetMenu(fileName = "GameSceneInstaller",
        menuName = "Zenject/New GameSceneInstaller")]
    public class GameSceneInstaller : ScriptableObjectInstaller
    {
        public override void InstallBindings()
        {
            GameStateSchedulerInstaller.Install(Container);
            InputInstaller.Install(Container);
            CameraControllerInstaller.Install(Container);
        }
    }
}