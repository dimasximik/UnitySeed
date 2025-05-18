using UnityEngine;

namespace Core.Services
{
    [System.Serializable]
    public class SaveData
    {
        public int level;
        public int xp;
        public int xpToNext;

        public int hp;
        public int speedLevel;
        public int damageLevel;
    }

    public class SaveService
    {
        const string KEY = "DEMO_SAVE";

        public void Save(SaveData data)
        {
            PlayerPrefs.SetString(KEY, JsonUtility.ToJson(data));
            PlayerPrefs.Save();
        }

        public SaveData Load()
        {
            if (!PlayerPrefs.HasKey(KEY)) return null;
            return JsonUtility.FromJson<SaveData>(PlayerPrefs.GetString(KEY));
        }

        public void Clear() => PlayerPrefs.DeleteKey(KEY);
    }
}