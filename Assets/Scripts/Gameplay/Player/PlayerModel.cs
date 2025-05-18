using Core.Services;
using UnityEngine;

namespace Gameplay.Player
{
    public class PlayerModel
    {
        public int Level        { get; private set; } = 1;
        public int SpeedLevel   { get; private set; }
        public int DamageLevel  { get; private set; }

        public int MaxHp { get; } = 5;
        public int Hp    { get; private set; } = 5;
        public bool IsDead => Hp <= 0;

        public int Xp       { get; private set; }
        public int XpToNext { get; private set; } = 3;

        public float MoveSpeed    => 4f + SpeedLevel  * 0.5f;
        public int   BulletDamage => 1   + DamageLevel;

        public void GainXp(int amount)
        {
            Xp += amount;
            while (Xp >= XpToNext)
            {
                Xp -= XpToNext;
                XpToNext += 3; 
                Level++;
            }
        }
        

        public SaveData ToSave()
        {
            return new SaveData
            {
                level        = Level,
                xp           = Xp,
                xpToNext     = XpToNext,
                hp           = Hp,
                speedLevel   = SpeedLevel,
                damageLevel  = DamageLevel
            };
        }

        public void FromSave(SaveData savedData)
        {
            Level       = savedData.level;
            Xp          = savedData.xp;
            XpToNext    = savedData.xpToNext;
            Hp          = savedData.hp;
            SpeedLevel  = savedData.speedLevel;
            DamageLevel = savedData.damageLevel;
        }


        public void UpgradeSpeed()  => SpeedLevel++;
        public void UpgradeDamage() => DamageLevel++;

        public void TakeDamage(int dmg) => Hp = Mathf.Max(0, Hp - dmg);

        public void Reset()
        {
            Level = 1;
            SpeedLevel = DamageLevel = 0;
            Hp = MaxHp;
            Xp = 0;
            XpToNext = 5;
        }
    }
}