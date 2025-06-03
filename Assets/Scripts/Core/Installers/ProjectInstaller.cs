using Core.Services;
using Core.Signals;
using Zenject;

namespace Core.Installers
{
    public class ProjectInstaller : MonoInstaller
    {

        public override void InstallBindings()
        {
            SignalBusInstaller.Install(Container);
            Container.DeclareSignal<LevelUpSignal>();
            Container.DeclareSignal<PlayerDamagedSignal>();
            Container.DeclareSignal<PlayerDiedSignal>();

            Container.BindInterfacesTo<SaveService>().AsSingle();
            Container.BindInterfacesTo<GameStateService>().AsSingle();
            Container.BindInterfacesTo<PlayerProgressService>().AsSingle();

        }
    }
}