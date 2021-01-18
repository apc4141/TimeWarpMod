using Zenject;

namespace TimeWarpMod.Main
{
    class MyGameInstaller : MonoInstaller
    { 
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<GameSpeedManager>().AsSingle();
        }    
    }
}
