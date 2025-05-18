// Assets/_Project/Presentation/GameScene/GameInstaller.cs

using Core.Services;
using Gameplay.Bullets;
using Gameplay.Enemy;
using Gameplay.Player;
using Presentation.Enemy;
using Presentation.Player;
using UnityEngine;
using Zenject;

namespace Presentation.Installers
{
    public class GameInstaller : MonoInstaller
    {
        [Header("Prefabs")]
        [SerializeField] EnemyView enemyPrefab;
        [SerializeField] Bullet bulletPrefab;


        public override void InstallBindings()
        {
            Container.Bind<IPlayerView>()
                .FromComponentInHierarchy()
                .AsSingle();

            Container.Bind<PlayerModel>()
                .FromMethod(_ => 
                    Container.Resolve<GameStateService>()
                        .Player)
                .AsSingle();

            Container.BindInterfacesAndSelfTo<PlayerPresenter>()
                .AsSingle().NonLazy();

            Container.BindMemoryPool<EnemyView, EnemyView.Pool>()
                .WithInitialSize(16)
                .FromComponentInNewPrefab(enemyPrefab)
                .UnderTransformGroup("Enemies");

            Container.BindInterfacesAndSelfTo<EnemySpawner>()
                .AsSingle()
                .NonLazy();
            
            
            Container.BindMemoryPool<Bullet, Bullet.Pool>()
                .WithInitialSize(32)
                .FromComponentInNewPrefab(bulletPrefab)
                .UnderTransformGroup("RuntimePools");
        }
    }
}