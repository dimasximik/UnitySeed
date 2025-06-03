namespace Core.Services
{
    public interface IGameStateService
    {
        Gameplay.Player.PlayerModel Player { get; }
        void DamagePlayer(int dmg);
        void UpgradeSpeed();
        void UpgradeDamage();
        void ResetGame();
    }
}
