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
        [SerializeField] private float _minXPosition;
        [SerializeField] private float _maxXPosition;

        public override void InstallBindings()
        {
            InputInstaller.Install(Container);
            CameraControllerInstaller.Install(Container);
            WorldBoundsInstaller.Install(Container, _minXPosition, _maxXPosition);
        }
    }
}