using Core.Signals;
using Gameplay.Player;
using Zenject;

namespace Core.Services
{
    public class PlayerProgressService : IPlayerProgressService
    {
        readonly PlayerModel               _player;
        readonly SignalBus                 _bus;
        readonly ISaveService<SaveData>    _save;

        [Inject]
        public PlayerProgressService(PlayerModel player,
            SignalBus bus,
            ISaveService<SaveData> save)
        {
            _player = player;
            _bus    = bus;
            _save   = save;
        }

        public void AddXp(int amount)
        {
            int before = _player.Level;
            _player.GainXp(amount);
            _save.Save(_player.ToSave());

            if (_player.Level > before)
                _bus.Fire<LevelUpSignal>();
        }
    }
}
