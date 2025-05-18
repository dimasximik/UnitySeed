using System;
using Core.Services;
using Core.Signals;
using Gameplay.Bullets;
using Presentation.Player;
using UnityEngine;
using Zenject;

namespace Gameplay.Player
{
    public class PlayerPresenter : ITickable, IInitializable, IDisposable
    {
        readonly IPlayerView         _view;
        readonly PlayerModel         _model;
        readonly Bullet.Pool         _pool;
        readonly SignalBus           _bus;

        float _fireCd;

        [Inject]
        public PlayerPresenter(IPlayerView view,
            GameStateService state,
            Bullet.Pool pool,
            SignalBus bus)
        {
            _view  = view;
            _model = state.Player;
            _pool  = pool;
            _bus   = bus;
        }

        public void Initialize() =>
            _bus.Subscribe<PlayerDamagedSignal>(_view.PlayDamageFx);

        public void Dispose() =>
            _bus.TryUnsubscribe<PlayerDamagedSignal>(_view.PlayDamageFx);

        public void Tick()
        {
            if (_model.IsDead) return;

            Move();
            Shoot();
        }

        void Move()
        {
            var dir = new Vector2(Input.GetAxis("Horizontal"),
                Input.GetAxis("Vertical")).normalized;
            _view.Body.linearVelocity = dir * _model.MoveSpeed;
        }

        void Shoot()
        {
            if ((_fireCd -= Time.deltaTime) > 0) return;
            if (!Input.GetMouseButton(0)) return;

            _fireCd = .2f;

            Vector2 dir = (Camera.main.ScreenToWorldPoint(Input.mousePosition)
                           - _view.GunMuzzle.position).normalized;

            var b = _pool.Spawn();
            b.Init(_view.GunMuzzle.position, dir, _model.BulletDamage);
            _view.PlayShootFx();
        }
    }
}