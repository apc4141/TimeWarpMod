using Zenject;

namespace TimeWarpMod.Main
{
    class GameInstaller : MonoInstaller
    { 
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<GameManager>().AsSingle();
        }    
    }
}
