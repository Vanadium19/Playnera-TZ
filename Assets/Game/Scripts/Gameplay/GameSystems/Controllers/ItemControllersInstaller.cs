using UnityEngine;
using Zenject;

namespace Game.GameSystems.Controllers
{
    public class ItemControllersInstaller : MonoInstaller
    {
        [SerializeField] private DragAndDropController _dragAndDropController;
        [SerializeField] private ItemCollisionController _itemCollisionController;

        public override void InstallBindings()
        {
            Container.Bind<DragAndDropController>()
                .FromInstance(_dragAndDropController)
                .AsSingle();

            Container.Bind<ItemCollisionController>()
                .FromInstance(_itemCollisionController)
                .AsSingle();
        }
    }
}