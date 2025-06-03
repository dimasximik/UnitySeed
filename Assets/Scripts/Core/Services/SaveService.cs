using UnityEngine;

namespace Core.Services
{
    public interface ISaveService<T>
    {
        void Save(T data);
        T Load();
        void Clear();
    }

    public class SaveService : ISaveService<SaveData>
    {
        const string KEY = "DEMO_SAVE";

        public void Save(SaveData data)
        {
            PlayerPrefs.SetString(KEY, JsonUtility.ToJson(data));
            PlayerPrefs.Save();
        }

        public SaveData Load()
        {
            if (!PlayerPrefs.HasKey(KEY))
                return null;
            return JsonUtility.FromJson<SaveData>(PlayerPrefs.GetString(KEY));
        }

        public void Clear() => PlayerPrefs.DeleteKey(KEY);
    }
}
