using UnityEngine;

namespace Presentation.Enemy
{
    public interface IEnemyView
    {
        void TakeDamage(int dmg);
        Transform transform { get; }
    }
}
