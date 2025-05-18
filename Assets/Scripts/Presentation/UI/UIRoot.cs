using Core.Services;
using Core.Signals;
using Presentation.UI.Windows;
using UnityEngine.SceneManagement;
using Zenject;

namespace Presentation.UI
{
    public class UIRoot : IInitializable
    {
        readonly LevelUpWindow.Factory _lvlFactory;
        readonly SignalBus             _bus;
        readonly GameStateService      _state;

        LevelUpWindow _lvlWin;

        public UIRoot(LevelUpWindow.Factory lvlFactory,
            SignalBus bus,
            GameStateService state)
        {
            _lvlFactory = lvlFactory;
            _bus        = bus;
            _state      = state;
        }

        public void Initialize()
        {
            _bus.Subscribe<LevelUpSignal>(ShowLevelUp);
            _bus.Subscribe<PlayerDiedSignal>(OnPlayerDied);
        }

        void ShowLevelUp()
        {
            _lvlWin ??= _lvlFactory.Create();
            _lvlWin.Open();
        }

        void OnPlayerDied()
        {
            _state.ResetGame();                
            SceneManager.LoadScene("MainMenu");
        }
    }
}