using System;
using Core.Signals;
using Gameplay.Bullets;
using Presentation.Player;
using UnityEngine;
using Zenject;
using Core.Services;

namespace Gameplay.Player
{
    public class PlayerPresenter : ITickable, IInitializable, IDisposable, IFixedTickable
    {
        readonly IPlayerView   _view;
        readonly PlayerModel   _model;
        readonly Bullet.Pool   _pool;
        readonly SignalBus     _bus;
        readonly IInputService _input;

        float _fireCooldown;

        [Inject]
        public PlayerPresenter(IPlayerView view,
            GameStateService state,
            Bullet.Pool pool,
            SignalBus bus,
            IInputService input)
        {
            _view  = view;
            _model = state.Player;
            _pool  = pool;
            _bus   = bus;
            _input = input;
        }

        public void Initialize() =>
            _bus.Subscribe<PlayerDamagedSignal>(_view.PlayDamageFx);

        public void Dispose() =>
            _bus.Unsubscribe<PlayerDamagedSignal>(_view.PlayDamageFx);

        
        public void FixedTick()
        {
            if (_model.IsDead) return;

            Move();
        }
        public void Tick()
        {
            if (_model.IsDead) return;

            Shoot();
        }

        void Move()
        {
            var dir = new Vector2(
                _input.GetAxis("Horizontal"),
                _input.GetAxis("Vertical")).normalized;
            _view.Body.linearVelocity = dir * _model.MoveSpeed;
        }

        void Shoot()
        {
            if ((_fireCooldown -= Time.deltaTime) > 0) return;
            if (!_input.GetMouseButton(0)) return;

            _fireCooldown = .2f;

            Vector2 dir = (Camera.main.ScreenToWorldPoint(_input.MousePosition)
                           - _view.GunMuzzle.position).normalized;

            var b = _pool.Spawn();
            b.Init(_view.GunMuzzle.position, dir, _model.BulletDamage);
            _view.PlayShootFx();
        }
    }
}