using Core.Services;
using LitMotion;
using Presentation.Player;
using UnityEngine;
using Zenject;

namespace Presentation.Enemy
{
    [RequireComponent(typeof(Collider2D))]
    public class EnemyView : MonoBehaviour
    {
        [Header("Stats")]
        [SerializeField] int   hp          = 3;
        [SerializeField] float moveSpeed   = 2f;
        [SerializeField] int   contactDmg  = 1;

        [Header("FX")]
        [SerializeField] ParticleSystem hitFx;

        [Inject] IPlayerView  _player;
        [Inject] GameStateService _state;

        MotionHandle _punch;

        void Update() => MoveToPlayer();

        public void TakeDamage(int dmg)
        {
            hp -= dmg;
            hitFx.Play();

            _punch.TryCancel();
            var start = transform.localScale;
            _punch = LMotion.Punch.Create(Vector3.zero, Vector3.one * .25f, .2f)
                .WithFrequency(10).WithDampingRatio(.4f)
                .Bind(d =>
                {
                    if (this != null)
                        transform.localScale = start + d;
                });

            if (hp <= 0) Die();
        }

        void Die()
        {
            _state.AddXp(1);     
            _punch.TryCancel();
            Destroy(gameObject);
        }

        void MoveToPlayer()
        {
            if (_player == null) return;

            Vector2 dir = ((Vector2)_player.Body.transform.position -
                           (Vector2)transform.position).normalized;

            transform.position += (Vector3)(dir * moveSpeed * Time.deltaTime);
        }

        void OnTriggerEnter2D(Collider2D col)
        {
            if (col.CompareTag("Player"))
                _state.DamagePlayer(contactDmg);
        }

        public class Pool : MonoMemoryPool<EnemyView>
        {
            protected override void OnDespawned(EnemyView enemyView)
            {
                enemyView.transform.localScale = Vector3.one; 
            }
        }

    }
}