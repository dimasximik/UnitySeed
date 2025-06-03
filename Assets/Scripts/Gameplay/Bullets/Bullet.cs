using Presentation.Enemy;
using UnityEngine;
using Zenject;

namespace Gameplay.Bullets
{
    [RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
    public class Bullet : MonoBehaviour
    {
        [SerializeField] float speed = 12f;
        [SerializeField] float lifeTime = 2f;
        [SerializeField] string enemyTag = "Enemy";

        Rigidbody2D _rb;
        Pool _pool;
        int _damage;
        float _lifeTimer;
        bool _isDespawned;

        [Inject]
        void Construct(Pool pool) => _pool = pool;

        void Awake() => _rb = GetComponent<Rigidbody2D>();

        public void Init(Vector2 position, Vector2 direction, int damage)
        {
            _isDespawned = false;
            transform.position = position;
            _damage = damage;
            _lifeTimer = lifeTime;
            _rb.linearVelocity = direction.normalized * speed;
        }

        void Update()
        {
            if ((_lifeTimer -= Time.deltaTime) <= 0f)
                Despawn();
        }

        void OnTriggerEnter2D(Collider2D col)
        {
            if (!col.CompareTag(enemyTag)) return;

            var enemy = col.GetComponent<IEnemyView>();
            if (enemy != null)
                enemy.TakeDamage(_damage);

            Despawn();
        }

        void Despawn()
        {
            if (_isDespawned) return;
            _isDespawned = true;
            _pool.Despawn(this);
        }

        public class Pool : MonoMemoryPool<Bullet>
        {
        }
    }
}