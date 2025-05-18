using Presentation.UI;
using Presentation.UI.Windows;
using Zenject;

namespace Presentation.Installers
{
    public class UIInstaller : MonoInstaller
    {
        public LevelUpWindow levelUpWindowPrefab;

        public override void InstallBindings()
        {

            Container.BindFactory<LevelUpWindow, LevelUpWindow.Factory>()
                .FromComponentInNewPrefab(levelUpWindowPrefab)
                .UnderTransformGroup("UICanvas");

            Container.BindInterfacesAndSelfTo<UIRoot>().AsSingle();
        }
    }
}