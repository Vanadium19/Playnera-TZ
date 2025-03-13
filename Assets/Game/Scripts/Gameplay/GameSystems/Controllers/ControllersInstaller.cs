using UnityEngine;
using Zenject;

namespace Game.GameSystems.Controllers
{
    public class ControllersInstaller : MonoInstaller
    {
        [SerializeField] private DragAndDropController _dragAndDropController;

        public override void InstallBindings()
        {
            Container.Bind<DragAndDropController>()
                .FromInstance(_dragAndDropController)
                .AsSingle();
        }
    }
}