
using System.Collections.Generic;
using Presentation.Enemy;
using Presentation.Player;
using UnityEngine;
using Zenject;

namespace Gameplay.Enemy
{
    public class EnemySpawner : ITickable
    {
        readonly EnemyView.Pool _pool;

        readonly float _interval = 3f;
        readonly float _radius   = 8f;
        readonly int   _maxAlive = 20;

        readonly List<EnemyView> _alive = new();

        float _timer;

        [Inject]
        public EnemySpawner(EnemyView.Pool pool)
        {
            _pool   = pool;
        }

        public void Tick()
        {
            _alive.RemoveAll(e => e == null);

            if (_alive.Count >= _maxAlive) return;

            if ((_timer += Time.deltaTime) >= _interval)
            {
                _timer = 0f;
                SpawnEnemy();
            }
        }

        void SpawnEnemy()
        {
            var cam = Camera.main;
            var viewportPos = new Vector2(Random.value, Random.value);    
            Vector2 worldPos = cam.ViewportToWorldPoint(viewportPos);

            var enemy = _pool.Spawn();
            enemy.transform.position = worldPos;
            _alive.Add(enemy);
        }

    }
}