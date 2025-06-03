using Core.Signals;
using Gameplay.Player;
using Zenject;

namespace Core.Services
{
    public class GameStateService : IGameStateService
    {
        readonly SignalBus   _bus;
        readonly ISaveService<SaveData> _save;

        public PlayerModel Player { get; private set; }

        public GameStateService(SignalBus bus, ISaveService<SaveData> save)
        {
            _bus  = bus;
            _save = save;

            var data = _save.Load();
            Player   = new PlayerModel();

            if (data != null)
                Player.FromSave(data);
        }


        public void DamagePlayer(int dmg)
        {
            Player.TakeDamage(dmg);
            SaveGame();

            _bus.Fire<PlayerDamagedSignal>();
            if (Player.IsDead)
                _bus.Fire<PlayerDiedSignal>();
        }

        public void UpgradeSpeed()  { Player.UpgradeSpeed(); SaveGame(); }
        public void UpgradeDamage() { Player.UpgradeDamage(); SaveGame(); }

        public void ResetGame()
        {
            Player.Reset();
            SaveGame();
        }

        void SaveGame() => _save.Save(Player.ToSave());
    }
}